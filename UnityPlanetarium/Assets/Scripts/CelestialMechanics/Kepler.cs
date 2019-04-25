using System;

namespace CelestialMechanics
{
    public static class Kepler
    {
        /// <summary>
        /// Computes the eccentric anomaly given the eccentricity and the mean
        /// anomaly using the iterative aproximation.
        /// </summary>
        /// <param name="e">The eccentricity.</param>
        /// <param name="M">The true anomaly.</param>
        /// <returns>The computed eccentric anomaly.</returns>
        public static double EccentricAnomaly(double e, double M, int iterations = 100)
        {
            var E = 0.0;
            for (int i = 0; i < iterations; i++)
                E = M + e * Math.Sin(E);
            return E;
        }
    }
}