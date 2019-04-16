using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CelestialMechanics;
using System;

public class OrbitLine : MonoBehaviour
{
    public float xposition=0.0f;
    public float yposition = 0.0f;
    private DateTime dateStart;

    private Orbit _orbit;

    // Start is called before the first frame update
    void Start()
    {
        dateStart = DateTime.Now;
        var elements = new OrbitalElements(1, 0, 0, 0, 0, 0, 1);
		_orbit = new Orbit(elements);
        var pointsToDraw = _orbit.OrbitPoints(100);
        LineRenderer line = GetComponent<LineRenderer>();
        line.positionCount = pointsToDraw.Length;
        for(int i = 0; i < pointsToDraw.Length; i++)
        {
            line.SetPosition(i, new Vector3(pointsToDraw[i].X, pointsToDraw[i].Z, pointsToDraw[i].Y));
        }
    }

    // Update is called once per frame
    void Update()
	{

        //float annee=(float)(DateTime.Now-dateStart).TotalSeconds/5;
		//var pos = _orbit.Position(annee);
        //transform.position=new Vector3(pos.X,pos.Z, pos.Y);
		
    }
}
