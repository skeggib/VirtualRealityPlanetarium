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
    public GameObject GrabSphere;

    public bool IsGrabbing { get; private set; }

    private Vector3 _lastPosition;

    void Start()
    {
        _lastPosition = transform.localPosition;
    }

    void Update()
    {
        var realGrabDistance = GrabDistance * World.transform.localScale.x;
        
        var test = realGrabDistance / GrabSphere.transform.lossyScale.x * 2;
        var temp = GrabSphere.transform.localScale;
        temp.Scale(new Vector3(test, test, test));
        GrabSphere.transform.localScale = temp;
        Debug.Log($"{name} \t{GrabSphere.transform.lossyScale.x}");

        var distance = Vector3.Distance(transform.position, Camera.transform.position);
        if (!(Camera is null) &&
            !(World is null) &&
            distance < realGrabDistance)
        {
            IsGrabbing = true;
            var translation = transform.localPosition - _lastPosition;
            translation.Scale(World.transform.localScale);
            World.transform.position -= translation;
        }
        else
        {
            IsGrabbing = false;
        }
        _lastPosition = transform.localPosition;
    }
}