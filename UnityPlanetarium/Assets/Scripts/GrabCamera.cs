using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CelestialMechanics;
using System;

public class GrabCamera : MonoBehaviour
{
    public Camera Camera;
    public GameObject World;
    public float GrabDistance = 1;

    public bool Grabbing { get; private set; }

    private Vector3 _lastPosition;

    void Start()
    {
        _lastPosition = transform.localPosition;
    }

    void Update()
    {
        if (!(Camera is null) &&
            !(World is null) &&
            Vector3.Distance(transform.position, Camera.transform.position) < GrabDistance)
        {
            Grabbing = true;
            World.transform.position -= transform.localPosition - _lastPosition;
        }
        else
        {
            Grabbing = false;
        }
        _lastPosition = transform.localPosition;
    }
}