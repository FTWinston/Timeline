using System;

namespace Timeline.Data.Model
{
    public struct Distribution
    {
        public Distribution(double mean, double standardDeviation)
        {
            Mean = mean;
            StandardDeviation = standardDeviation;
        }

        public double Mean { get; private set; }
        public double StandardDeviation { get; private set; }

        public double Get(Random r)
        {
            double u1 = r.NextDouble(), u2 = r.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return Mean + StandardDeviation * randStdNormal;
        }
    }
}
