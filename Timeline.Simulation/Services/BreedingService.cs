using System;
using System.Linq;
using Timeline.Data.Model;
using Timeline.Data.Services;

namespace Timeline.Simulation.Services
{
    public class BreedingService
    {
        RandomService RandomService { get; }

        public BreedingService(RandomService randomService)
        {
            RandomService = randomService;
        }

        public Person Reproduce(GameTime date, Person mother, Person father)
        {
            if (!CanReproduce(mother, father))
                return null;

            int baseSeed = mother.RandomStart ^ father.RandomStart; // is this a good or sensible way to combine seeds?
            int numFullSiblings = mother.Children.Count(p => p.Father == father);
            var randomStart = baseSeed + numFullSiblings;
            var randomIncrement = Math.Abs(mother.RandomIncrement - father.RandomIncrement);
            if (randomIncrement == 0)
                randomIncrement = mother.RandomIncrement;

            var childRace = mother.Race.FertilityChances[father.Race].Item2;
            var gender = RandomService.GetNextBool(mother) ? Gender.Female : Gender.Male;

            var child = new Person(randomStart, randomIncrement, childRace, gender, mother, father, date);
            return child;
        }

        public bool CanReproduce(Person mother, Person father)
        {
            return mother.Race.FertilityChances.ContainsKey(father.Race);
        }
    }
}
