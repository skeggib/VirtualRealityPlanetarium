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

    public float Year { get; private set; }

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
        Year = (float)(DateTime.Now-_startDate).TotalSeconds / 100;
		var pos = _orbit.Position(Year);
        pos.Scale(transform.parent.localScale);
        transform.position = transform.parent.position + pos;
    }
}
