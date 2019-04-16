using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CelestialMechanics;
using System;

public class MovePlanet : MonoBehaviour
{
    public float MajorRadius;
    public float MeanLongitudeAtEpoch;
    public float Eccentricity;
    public float InclinationToElliptic;
    public float LongitudeOfPerihelion;
    public float LongitudeOfAscendingNode;
    public float OrbitalPeriod;

    private DateTime _startDate;
    private Orbit _orbit;

    // Start is called before the first frame update
    void Start()
    {
        _startDate = DateTime.Now;
        var elements = new OrbitalElements(
            MajorRadius,
            MeanLongitudeAtEpoch,
            Eccentricity,
            InclinationToElliptic,
            LongitudeOfPerihelion,
            LongitudeOfAscendingNode,
            OrbitalPeriod);
		_orbit = new Orbit(elements);
    }

    // Update is called once per frame
    void Update()
	{
        float year = (float)(DateTime.Now-_startDate).TotalSeconds / 15;
		var pos = _orbit.Position(year);
        transform.position = transform.parent.position + new Vector3(pos.X, pos.Z, pos.Y);
    }
}
