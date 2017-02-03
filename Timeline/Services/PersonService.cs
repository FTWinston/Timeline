using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Model;

namespace Timeline.Services
{
    static class PersonService
    {
        private static object newChildMutex = new object();

        public static void SimulateYear(Person person, List<Person> eligibleBachelors)
        {
            if (person.IsDead)
                return;

            var date = person.World.Date;
            if (ShouldDie(person, date))
            {
                person.Death = date;
                return;
            }

            if (ShouldBearYoung(person))
            {
                var mate = ChooseMate(person, eligibleBachelors);
                if (mate != null)
                {
                    var child = BreedingService.Reproduce(person, mate);
                    if (child != null)
                        lock (newChildMutex)
                        {
                            person.Children.Add(child);
                            mate.Children.Add(child);
                            person.World.NewPeople.Add(child);
                        }
                }
            }
        }

        private static bool ShouldDie(Person person, GameTime date)
        {
            // TODO: use distribution, not fixed racial lifespan mean
            return date.Ticks >= person.Birth.Ticks + person.Race.Lifespan.Mean;
        }

        public static bool IsChildBearingAge(Person person)
        {
            // TODO: use distribution, avoid casting etc
            if (person.Age < new GameTimeSpan() { Ticks = (long)person.Race.MinChildBearingAge.Mean })
                return false;
            if (person.Age > new GameTimeSpan() { Ticks = (long)person.Race.MaxChildBearingAge.Mean })
                return false;

            return true;
        }

        private static bool ShouldBearYoung(Person person)
        {
            if (person.Gender != Gender.Female)
                return false;

            if (!IsChildBearingAge(person))
                return false;

            // this is only reproducable if this is called exactly once per year
            return person.Random.NextDouble() < person.Race.FertilityRate;
        }

        private static object mateChoiceMutex = new object();

        private static Person ChooseMate(Person potentialMother, List<Person> eligibleBachelors)
        {
            for (int iAttempt = 0; iAttempt < 3; iAttempt++)
            {
                lock (mateChoiceMutex)
                {
                    // pick one at random and make sure you're not related
                    var choiceNumber = potentialMother.Random.Next(eligibleBachelors.Count);
                    var potentialMale = eligibleBachelors[choiceNumber];

                    if (AllowedToMate(potentialMother, potentialMale))
                    {
                        eligibleBachelors.RemoveAt(choiceNumber);
                        return potentialMale;
                    }
                }
            }

            return null;
        }

        private static bool AllowedToMate(Person female, Person male)
        {
            if (female.Father == male || male.Mother == female)
                return false;

            if (male.Father == female.Father && female.Father != null)
                return false;

            if (male.Mother == female.Mother && female.Mother != null)
                return false;

            return true;
        }
    }
}
