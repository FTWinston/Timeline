using System;
using Timeline.Data.Model;

namespace Timeline.Data.Services
{
    public class RandomService
    {
        int[] Randomness { get; }

        public int Length => Randomness.Length;

        public RandomService(int seed)
        {
            Randomness = PopulateRandomness(seed);
        }

        public int[] PopulateRandomness(int seed)
        {
            var random = new Random(seed);
            var randomness = new int[10000];
            for (int i = 0; i < randomness.Length; i++)
                randomness[i] = random.Next(int.MinValue, int.MaxValue);

            return randomness;
        }

        private int GetNext(Randomizable randomizable)
        {
            randomizable.CurrentRandomPos += randomizable.RandomIncrement;
            if (randomizable.CurrentRandomPos >= Randomness.Length)
                randomizable.CurrentRandomPos -= Randomness.Length;
            return Randomness[randomizable.CurrentRandomPos];
        }

        private int GetPrevious(Randomizable randomizable)
        {
            randomizable.CurrentRandomPos -= randomizable.RandomIncrement;
            if (randomizable.CurrentRandomPos < 0)
                randomizable.CurrentRandomPos += Randomness.Length;
            return Randomness[randomizable.CurrentRandomPos];
        }

        private int Modulo(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        public bool GetNextBool(Randomizable randomizable)
        {
            return GetNext(randomizable) % 2 == 0;
        }

        public bool GetPrevBool(Randomizable randomizable)
        {
            return GetPrevious(randomizable) % 2 == 0;
        }

        public int GetNextInt(Randomizable randomizable, int min, int max)
        {
            return Modulo(GetNext(randomizable), max - min) + min;
        }

        public int GetPrevInt(Randomizable randomizable, int min, int max)
        {
            return Modulo(GetPrevious(randomizable), max - min) + min;
        }
    }
}
