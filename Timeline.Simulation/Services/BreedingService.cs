using System;
using System.Linq;
using Timeline.Data.Model;

namespace Timeline.Simulation.Services
{
    public static class BreedingService
    {
        public static Person Reproduce(Person mother, Person father)
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
            var birthDate = mother.World.Date;

            var child = new Person(mother.World, randomStart, randomIncrement, childRace, gender, mother, father, birthDate);
            return child;
        }

        public static bool CanReproduce(Person mother, Person father)
        {
            return mother.Race.FertilityChances.ContainsKey(father.Race);
        }
    }
}
