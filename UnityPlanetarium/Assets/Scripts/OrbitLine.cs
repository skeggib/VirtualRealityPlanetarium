using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CelestialMechanics;
using System;

public class OrbitLine : MonoBehaviour
{
    public GameObject Planet;
    
    private DateTime dateStart;

    private Orbit _orbit;

    // Start is called before the first frame update
    void Start()
    {
        var movePlanet = Planet.GetComponent<MovePlanet>();

        dateStart = DateTime.Now;
        var elements = new OrbitalElements(
            movePlanet.MajorRadius,
            movePlanet.MeanLongitudeAtEpoch,
            movePlanet.Eccentricity,
            movePlanet.InclinationToElliptic,
            movePlanet.LongitudeOfPerihelion,
            movePlanet.LongitudeOfAscendingNode,
            movePlanet.OrbitalPeriod
        );
		_orbit = new Orbit(elements);
        var pointsToDraw = _orbit.OrbitPoints(100);
        LineRenderer line = GetComponent<LineRenderer>();
        line.positionCount = pointsToDraw.Length+1;
        for(int i = 0; i < pointsToDraw.Length; i++)
            line.SetPosition(i, pointsToDraw[i]);
        line.SetPosition(line.positionCount-1, pointsToDraw[0]);
    }

    // Update is called once per frame
    void Update()
	{

    }
}
