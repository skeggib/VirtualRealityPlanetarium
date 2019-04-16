using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Zooming : MonoBehaviour
{
    //public SteamVR_Input_Sources handType;
    
    public SteamVR_Input_Sources handRightsource;
    public SteamVR_Input_Sources handLeftsource;

    public Hand handRight;
    public Hand handLeft;

    bool zooming = false;

    Vector3 currentCenterControllerPosition;
    float currentDistanceControllers;
    Vector3 currentScale;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Valve.VR.EVRButtonId.k_EButton_Grip;
        
        if (handRight.grabGripAction.GetState(handRightsource) && handLeft.grabGripAction.GetState(handLeftsource))
        {
            //Debug.Log("The two grips are pressed");

            currentScale = transform.localScale;
            currentCenterControllerPosition = Vector3.Lerp(handLeft.transform.position, handRight.transform.position, 0.5f);
            currentDistanceControllers = Vector3.Distance(handLeft.transform.position, handRight.transform.position);

            zooming = true;
        }

        if (!handRight.grabGripAction.GetState(handRightsource) || !handLeft.grabGripAction.GetState(handLeftsource))
        {
            //Debug.Log("The two grips are Unpressed");

            zooming = false;
        }

        if (zooming)
        {
            float nextDistanceControllers = Vector3.Distance(handLeft.transform.position, handRight.transform.position);
            float scale = (nextDistanceControllers / currentDistanceControllers);

            Debug.Log("scale = " + scale);

            this.transform.position = new Vector3(scale * transform.localScale.x, scale * transform.localScale.y, scale * transform.localScale.z);
            //scaleSolarSystem();
        }
    }

    private void scaleSolarSystem()
    {
        //Debug.Log("Left hand position : " + handLeft.transform.position + " ; Right hand position : " + handRight.transform.position);
        float nextDistanceControllers = Vector3.Distance(handLeft.transform.position, handRight.transform.position);
        float scale = (nextDistanceControllers / currentDistanceControllers);

        Debug.Log("scale = " + scale);

        transform.localScale = new Vector3(scale * transform.localScale.x, scale * transform.localScale.y, scale * transform.localScale.z);

        //Vector3 prePositionRight = 
    }
}
