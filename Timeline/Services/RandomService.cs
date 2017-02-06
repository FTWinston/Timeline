using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Model;

namespace Timeline.Services
{
    public static class RandomService
    {
        public static int[] PopulateRandomness(int seed)
        {
            var random = new Random(seed);
            var randomness = new int[10000];
            for (int i = 0; i < randomness.Length; i++)
                randomness[i] = random.Next(int.MinValue, int.MaxValue);

            return randomness;
        }

        private static int GetNext(Randomizable randomizable)
        {
            var config = randomizable.Configuration;
            randomizable.CurrentRandomPos += randomizable.RandomIncrement;
            if (randomizable.CurrentRandomPos >= config.Randomness.Length)
                randomizable.CurrentRandomPos -= config.Randomness.Length;
            return config.Randomness[randomizable.CurrentRandomPos];
        }

        private static int GetPrevious(Randomizable randomizable)
        {
            var config = randomizable.Configuration;
            randomizable.CurrentRandomPos -= randomizable.RandomIncrement;
            if (randomizable.CurrentRandomPos < 0)
                randomizable.CurrentRandomPos += config.Randomness.Length;
            return config.Randomness[randomizable.CurrentRandomPos];
        }

        public static bool GetNextBool(Randomizable randomizable)
        {
            return GetNext(randomizable) % 2 == 0;
        }

        public static bool GetPrevBool(Randomizable randomizable)
        {
            return GetPrevious(randomizable) % 2 == 0;
        }

        public static int GetNextInt(Randomizable randomizable, int min, int max)
        {
            return (GetNext(randomizable) % (max - min)) + min;
        }

        public static int GetPrevInt(Randomizable randomizable, int min, int max)
        {
            return (GetPrevious(randomizable) % (max - min)) + min;
        }
    }
}
