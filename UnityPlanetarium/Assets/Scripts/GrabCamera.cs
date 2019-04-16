using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CelestialMechanics;
using System;

public class GrabCamera : MonoBehaviour
{
    public Camera Camera;

    public float GrabDistance = 1;

    public bool Grabbing { get; private set; }

    private Vector3 _lastPosition;

    void Start()
    {
        _lastPosition = transform.position;
    }

    void Update()
    {
        var currentPosition = transform.position;
        if (!(Camera is null) &&
            Vector3.Distance(currentPosition, Camera.transform.position) < GrabDistance &&
            currentPosition != _lastPosition)
        {
            Grabbing = true;
            Camera.transform.position += currentPosition - _lastPosition;
        }
        else
        {
            Grabbing = false;
        }
        _lastPosition = currentPosition;
    }
}