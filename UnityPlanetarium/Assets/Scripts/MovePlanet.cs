using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CelestialMechanics;
using System;

public class MovePlanet : MonoBehaviour
{
    public GameObject Globals;
    private Globals _globalsScript;

    public float MajorRadius;
    public float MeanLongitudeAtEpoch;
    public float Eccentricity;
    public float InclinationToElliptic;
    public float LongitudeOfPerihelion;
    public float LongitudeOfAscendingNode;
    public float OrbitalPeriod;

    private Orbit _orbit;

    // Start is called before the first frame update
    void Start()
    {
        _globalsScript = Globals.GetComponent<Globals>();

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
		var pos = _orbit.Position(_globalsScript.GetComponent<TimeManipulation>().Year);
        pos.Scale(transform.parent.localScale);
        transform.position = transform.parent.position + pos;
    }
}
