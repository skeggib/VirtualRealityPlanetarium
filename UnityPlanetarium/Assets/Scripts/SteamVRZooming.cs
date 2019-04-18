using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SteamVRZooming : MonoBehaviour
{
    //public SteamVR_Input_Sources handType;
    
    public SteamVR_Input_Sources handRightsource;
    public SteamVR_Input_Sources handLeftsource;

    public Camera camera;

    public Hand handRight;
    public Hand handLeft;

    bool zooming = false;

    Vector3 initialCenterControllerPosition;
    float initialDistanceControllers;
    Vector3 initialScale;

    Vector3 initialPosition;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (handRight.grabGripAction.GetState(handRightsource) && handLeft.grabGripAction.GetState(handLeftsource) && !zooming)
        {
            initialScale = transform.localScale;
            initialPosition = transform.position;
            initialDistanceControllers = Vector3.Distance(handLeft.transform.localPosition, handRight.transform.localPosition);

            
            initialCenterControllerPosition = Vector3.Lerp(handLeft.transform.position, handRight.transform.position, 0.5f);
            Debug.Log("initialCenterControllerPosition" + initialCenterControllerPosition);
            Debug.Log("Camera Position" + camera.transform.position);
            
            zooming = true;
        }

        if (!handRight.grabGripAction.GetState(handRightsource) || !handLeft.grabGripAction.GetState(handLeftsource))
        {
            zooming = false;
        }

        if (zooming)
        {
            scaleSolarSystem();
        }
    }

    private void scaleSolarSystem()
    {
        float currentDistanceControllers = Vector3.Distance(handLeft.transform.localPosition, handRight.transform.localPosition);
        float scale = (currentDistanceControllers / initialDistanceControllers);

        Vector3 newScale = new Vector3(initialScale.x * scale, initialScale.y * scale, initialScale.z * scale);
        transform.localScale =  newScale;

        transform.position = initialPosition + initialCenterControllerPosition - new Vector3(initialPosition.x - initialCenterControllerPosition.x * scale, initialPosition.y - initialCenterControllerPosition.y * scale, initialPosition.y - initialCenterControllerPosition.y * scale);
    }
}
