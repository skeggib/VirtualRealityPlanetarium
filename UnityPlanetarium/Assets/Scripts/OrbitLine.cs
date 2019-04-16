﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CelestialMechanics;
using System;

public class OrbitLine : MonoBehaviour
{
    public GameObject Planet;

    public Camera Camera;
    
    private Orbit _orbit;

    // Start is called before the first frame update
    void Start()
    {
        var movePlanet = Planet.GetComponent<MovePlanet>();
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
    }

    // Update is called once per frame
    void Update()
	{
        var lineRenderer = GetComponent<LineRenderer>();
        var grabCamera = Planet.GetComponent<GrabCamera>();
        if (grabCamera is null || !grabCamera.Grabbing)
        {
            var movePlanet = Planet.GetComponent<MovePlanet>();
            var pointsToDraw = _orbit.OrbitPoints(movePlanet.Year, movePlanet.OrbitalPeriod * 0.3f, 100);
            lineRenderer.positionCount = pointsToDraw.Length+1;
            for(int i = 0; i < pointsToDraw.Length; i++)
                lineRenderer.SetPosition(i, pointsToDraw[i]);
            lineRenderer.SetPosition(lineRenderer.positionCount-1, Planet.transform.position);
            var distance = Vector3.Distance(Planet.transform.position, Camera.transform.position);
            lineRenderer.startWidth = distance / 1000;
            lineRenderer.endWidth = distance / 300;
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }
}
