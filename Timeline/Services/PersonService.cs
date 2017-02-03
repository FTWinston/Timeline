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
        public static void SimulateYear(Person person)
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
                var mate = ChooseMate(person);
                if (mate != null)
                {
                    var child = BreedingService.Reproduce(person, mate);
                    if (child != null)
                        person.World.NewPeople.Add(child);
                }
            }
        }

        private static bool ShouldDie(Person person, GameTime date)
        {
            // TODO: use distribution, not fixed racial lifespan mean
            return date.Ticks >= person.Birth.Ticks + person.Race.Lifespan.Mean;
        }

        private static bool IsChildBearingAge(Person person)
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

        private static Person ChooseMate(Person potentialMother)
        {
            // find a mate for a woman. Essentially, pick a living male of childbearing age.

            var availableMales = potentialMother.World.LivingPeople
                .Where(candidate => candidate.Gender == Gender.Male && IsChildBearingAge(candidate))
                .Where(candidate => candidate != potentialMother.Father && candidate.Mother != potentialMother) // no to Oedipus and Electra
                .Where(candidate => (candidate.Father == null || candidate.Father != potentialMother.Father) && (candidate.Mother == null || candidate.Mother != potentialMother.Mother)) // no siblings or half siblings
                ;

            if (!availableMales.Any())
                return null;
            
            int choiceNum = potentialMother.Random.Next(availableMales.Count());
            return availableMales.ElementAt(choiceNum);
        }
    }
}
