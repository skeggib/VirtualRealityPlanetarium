using System;

namespace CelestialMechanics
{
    /// <summary>
    /// Orbital elements of a celestial body that allows to compute it's
    /// position on it's orbit.
    /// </summary>
    public class OrbitalElements
    {
        /// <summary>
        /// Major radius (a, AU).
        /// </summary>
        public float MajorRadius { get; set; }

        /// <summary>
        /// Mean longitude at t=0 (lambda, °).
        /// </summary>
        /// <value></value>
        public float MeanLongitudeAtEpoch { get; set; }

        private float _eccentricity;
        /// <summary>
        /// Eccentricity of the ellipse (e).
        /// </summary>
        /// <value></value>
        public float Eccentricity
        {
            get => _eccentricity;
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException("value",
                        "An eccentricity muste be between 0 (included) and 1 (included).");
                _eccentricity = value;
            }
        }

        /// <summary>
        /// Inclination to elliptic (I, °).
        /// </summary>
        /// <value></value>
        public float InclinationToElliptic { get; set; }

        /// <summary>
        /// Longitude of perihelion (omega, °).
        /// </summary>
        /// <value></value>
        public float LongitudeOfPerihelion { get; set; }

        /// <summary>
        /// Longitude of ascending node (Omega, °).
        /// </summary>
        /// <value></value>
        public float LongitudeOfAscendingNode { get; set; }

        /// <summary>
        /// Orbital period (T, years).
        /// </summary>
        /// <value></value>
        public float OrbitalPeriod { get; set; }

        /// <summary>
        /// Creates an <see cref="OrbitalElements"> object.
        /// </summary>
        /// <param name="majorRadius">Major radius (a, AU).</param>
        /// <param name="meanLongitudeAtEpoch">Mean longitude at t=0 (lambda, °).</param>
        /// <param name="eccentricity">Eccentricity of the ellipse (e).</param>
        /// <param name="inclinationToElliptic">Inclination to elliptic (I, °).</param>
        /// <param name="longitudeOfPerihelion">Longitude of perihelion (omega, °).</param>
        /// <param name="longitudeOfAscendingNode">Longitude of ascending node (Omega, °).</param>
        /// <param name="orbitalPeriod">Orbital period (T, years).</param>
        public OrbitalElements(
            float majorRadius,
            float meanLongitudeAtEpoch,
            float eccentricity,
            float inclinationToElliptic,
            float longitudeOfPerihelion,
            float longitudeOfAscendingNode,
            float orbitalPeriod
        )
        {
            MajorRadius = majorRadius;
            MeanLongitudeAtEpoch = meanLongitudeAtEpoch;
            Eccentricity = eccentricity;
            InclinationToElliptic = inclinationToElliptic;
            LongitudeOfPerihelion = longitudeOfPerihelion;
            LongitudeOfAscendingNode = longitudeOfAscendingNode;
            OrbitalPeriod = orbitalPeriod;
        }
    }
}
