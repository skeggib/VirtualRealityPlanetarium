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
            scaleSolarSystem();
            Debug.Log("The two grips are pressed");
        }
    }

    private void scaleSolarSystem()
    {
              
        //Vector3 prePositionRight = 
    }
}
