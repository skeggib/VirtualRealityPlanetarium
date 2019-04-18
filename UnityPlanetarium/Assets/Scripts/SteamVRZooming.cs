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

    public Hand handRight;
    public Hand handLeft;

    bool zooming = false;

    Vector3 currentCenterControllerPosition;
    float currentDistanceControllers;
    Vector3 currentScale;

    Vector3 initialhandLeftPosition;
    Vector3 initialhandRightPosition;

    Quaternion initialObjectRotation;

    Vector3 initialObjectDirection;

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

            initialhandLeftPosition = handLeft.transform.position; // initial first hand position
            initialhandRightPosition = handRight.trackedObject.transform.position; // initial second hand position

            initialObjectRotation = this.transform.rotation;

            initialObjectDirection = this.transform.position - (initialhandLeftPosition + initialhandRightPosition) * 0.5f;

            //

            currentScale = this.transform.localScale;
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
        //float nextDistanceControllers = Vector3.Distance(handLeft.transform.position, handRight.transform.position);
        //float scale = (nextDistanceControllers / currentDistanceControllers);

        //Debug.Log("scale = " + scale);

        //transform.localScale = new Vector3(scale * transform.localScale.x, scale * transform.localScale.y, scale * transform.localScale.z);

        //

        Vector3 handLeftPosition = handLeft.transform.position; // current first hand position
        Vector3 handRightPosition = handRight.trackedObject.transform.position; // current second hand position

        Vector3 handDir1 = (initialhandLeftPosition - initialhandRightPosition).normalized; // direction vector of initial first and second hand position
        Vector3 handDir2 = (handLeftPosition - handRightPosition).normalized; // direction vector of current first and second hand position 

        Quaternion handRot = Quaternion.FromToRotation(handDir1, handDir2); // calculate rotation based on those two direction vectors


        float currentGrabDistance = Vector3.Distance(handLeftPosition, handRightPosition);
        float initialGrabDistance = Vector3.Distance(initialhandLeftPosition, initialhandRightPosition);
        float p = (currentGrabDistance / initialGrabDistance); // percentage based on the distance of the initial positions and the new positions

        Debug.Log("scale = " + p);
        Vector3 newScale = new Vector3(p * currentScale.x, p * currentScale.y, p * currentScale.z); // calculate new object scale with p

        this.transform.rotation = handRot * initialObjectRotation; // add rotation
        this.transform.localScale = newScale; // set new scale
        // set the position of the object to the center of both hands based on the original object direction relative to the new scale and rotation
        this.transform.position = (0.5f * (handLeftPosition + handRightPosition)) + (handRot * (initialObjectDirection * p));
    }
    

        
        
}
