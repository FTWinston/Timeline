using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Model;

namespace Timeline.Services
{
    static class BreedingService
    {
        internal static Person Reproduce(Person mother, Person father)
        {
            int baseSeed = mother.RandomStart ^ father.RandomStart; // is this a good or sensible way to combine seeds?
            int numFullSiblings = mother.Children.Count(p => p.Father == father);
            var randomStart = baseSeed + numFullSiblings;
            var randomIncrement = Math.Abs(mother.RandomIncrement - father.RandomIncrement);
            if (randomIncrement == 0)
                randomIncrement = mother.RandomIncrement;

            var race = mother.Race; // TODO: combining race, etc
            var gender = RandomService.GetNextInt(mother, 0, 2) == 0 ? Gender.Female : Gender.Male;
            var birthDate = mother.World.Date;

            var child = new Person(mother.World, randomStart, randomIncrement, race, gender, mother, father, birthDate);
            return child;
        }
    }
}
