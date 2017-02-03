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
            int baseSeed = mother.Seed ^ father.Seed; // is this a good or sensible way to combine seeds?
            int numFullSiblings = mother.Children.Count(p => p.Father == father);
            var seed = baseSeed + numFullSiblings;

            var race = mother.Race; // TODO: combining race, etc
            var gender = mother.Random.Next(2) == 0 ? Gender.Female : Gender.Male;
            var birthDate = mother.World.Date;

            var child = new Person(mother.World, seed, race, gender, mother, father, birthDate);
            mother.Children.Add(child);
            father.Children.Add(child);
            return child;
        }
    }
}
