using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CelestialMechanics;
using System;

public class GrabCamera : MonoBehaviour
{
    public GameObject Globals;
    private Globals _globalsScript;

    public float GrabDistance = 1;
    public GameObject GrabSphere;

    public bool IsGrabbing { get; private set; }

    private Vector3 _lastPosition;

    void Start()
    {
        _globalsScript = Globals.GetComponent<Globals>();
        _lastPosition = transform.localPosition;
    }

    void Update()
    {
        var realGrabDistance = GrabDistance * _globalsScript.World.transform.localScale.x;
        
        var test = realGrabDistance / GrabSphere.transform.lossyScale.x * 2;
        var temp = GrabSphere.transform.localScale;
        temp.Scale(new Vector3(test, test, test));
        GrabSphere.transform.localScale = temp;
        Debug.Log($"{name} \t{GrabSphere.transform.lossyScale.x}");

        var distance = Vector3.Distance(transform.position, _globalsScript.Camera.transform.position);
        if (!(_globalsScript.Camera is null) &&
            !(_globalsScript.World is null) &&
            distance < realGrabDistance)
        {
            IsGrabbing = true;
            var translation = transform.localPosition - _lastPosition;
            translation.Scale(_globalsScript.World.transform.localScale);
            _globalsScript.World.transform.position -= translation;
        }
        else
        {
            IsGrabbing = false;
        }
        _lastPosition = transform.localPosition;
    }
}