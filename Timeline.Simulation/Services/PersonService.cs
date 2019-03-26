using System.Collections.Generic;
using Timeline.Data.Model;
using Timeline.Data.Services;

namespace Timeline.Simulation.Services
{
    public class PersonService
    {
        RandomService RandomService { get; }
        BreedingService BreedingService { get; }

        private readonly object deathMutex = new object();
        private readonly object newChildMutex = new object();

        public PersonService(RandomService randomService, BreedingService breedingService)
        {
            RandomService = randomService;
            BreedingService = breedingService;
        }

        public void SimulateYear(Person person, Queue<Person> eligibleBachelors)
        {
            if (person.IsDead)
                return;

            var date = person.World.Date;
            if (ShouldDie(person, date))
            {
                lock (deathMutex)
                {
                    person.World.DeadPeople.Add(person);
                }

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

        private bool ShouldDie(Person person, GameTime date)
        {
            // TODO: use distribution, not fixed racial lifespan mean
            return date.Ticks >= person.Birth.Ticks + person.Race.Lifespan.Mean;
        }

        public bool IsChildBearingAge(Person person)
        {
            // TODO: use distribution, avoid casting etc
            if (person.Age < new GameTimeSpan((long)person.Race.MinChildBearingAge.Mean))
                return false;
            if (person.Age > new GameTimeSpan((long)person.Race.MaxChildBearingAge.Mean))
                return false;

            return true;
        }

        private bool ShouldBearYoung(Person person)
        {
            if (person.Gender != Gender.Female)
                return false;

            if (!IsChildBearingAge(person))
                return false;
            
            // TODO: Argh, these fertility chances need to know the partner's race. Should we separate chance of attempt from chance of success?
            return RandomService.GetNextInt(person, 0, person.Race.FertilityChances[person.Race].Item1) == 0;
        }

        private static object mateChoiceMutex = new object();

        private static Person ChooseMate(Person potentialMother, Queue<Person> eligibleBachelors)
        {
            for (int iAttempt = 0; iAttempt < 3; iAttempt++)
            {
                Person potentialFather;
                lock (mateChoiceMutex)
                {
                    //if (!eligibleBachelors.Any()) skipping this check hasn't caused any errors so far
                        //return null;
                    potentialFather = eligibleBachelors.Dequeue();
                }

                if (AllowedToMate(potentialMother, potentialFather))
                    return potentialFather;
                else
                    lock (mateChoiceMutex)
                    {   
                        eligibleBachelors.Enqueue(potentialFather);
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
