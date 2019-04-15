using System;
using System.Collections.Generic;

namespace CelestialMechanics
{
    public class Orbit
    {
        private DateTime J2000 => new DateTime(2000, 1, 1, 0, 0, 0);

        public double ExactDaysInYear => 365.2422;

        public OrbitalElements Elements { get; set; }

        public Orbit(OrbitalElements elements)
        {
            Elements = elements;
        }

        /// <summary>
        /// Computes the position of a celestial body on it's orbit at a given time.
        /// </summary>
        /// <param name="time">Time offset relative to J2000 (1st January 2000) in years.</param>
        /// <returns>The position of the celestial body.</returns>
        public Vec3 Position(double time)
        {
            var a = Elements.MajorRadius;
            var lambda = Elements.MeanLongitudeAtEpoch;
            var e = Elements.Eccentricity;
            var I = Elements.InclinationToElliptic / 180.0 * Math.PI;
            var omega = Elements.LongitudeOfPerihelion / 180.0 * Math.PI;
            var Omega = Elements.LongitudeOfAscendingNode / 180.0 * Math.PI;
            var T = Elements.OrbitalPeriod;

            var n = 2.0 * Math.PI / T;
            var theta = (lambda + omega) / n;
            var M = n * (time - theta);
            
            var E = Kepler.EccentricAnomaly(e, M);

            var Theta = 2.0 * Math.Atan(Math.Sqrt((1.0+e)/(1.0-e)) * Math.Tan(E/2.0));
            var r = a * (1.0 - e * Math.Cos(E));
            
            var position = new Vec3();
            position.X = r * (Math.Cos(Omega) * Math.Cos(omega + Theta) - Math.Sin(Omega) * Math.Sin(omega + Theta) * Math.Cos(I));
            position.Y = r * (Math.Sin(Omega) * Math.Cos(omega + Theta) - Math.Cos(Omega) * Math.Sin(omega + Theta) * Math.Cos(I));
            position.Z = r * Math.Sin(omega + Theta) * Math.Sin(I);

            return position;
        }

        /// <summary>
        /// Computes the position of a celestial body on it's orbit at a given time.
        /// </summary>
        /// <param name="time">The time at which the position will be computed.</param>
        /// <returns>The position of the celestial body.</returns>
        public Vec3 Position(DateTime time)
            => Position((time - J2000).TotalDays / ExactDaysInYear);

        /// <summary>
        /// Computes a given number of points on an orbit.
        /// </summary>
        /// <param name="pointsNumber">The number of points to compute.</param>
        /// <returns>The computed points.</returns>
        public Vec3[] OrbitPoints(int pointsNumber)
        {
            var points = new List<Vec3>();
            for (double t = 0; t < Elements.OrbitalPeriod; t += Elements.OrbitalPeriod / pointsNumber)
                points.Add(Position(t));
            return points.ToArray();
        }
    }
}