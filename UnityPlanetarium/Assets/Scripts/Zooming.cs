using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Zooming : MonoBehaviour
{
    //public SteamVR_Input_Sources handType;
    private Hand hand;
    public SteamVR_Action_Boolean gripAction;
    private SteamVR_Input_Sources handRight = SteamVR_Input_Sources.RightHand;
    private SteamVR_Input_Sources handLeft = SteamVR_Input_Sources.LeftHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Valve.VR.EVRButtonId.k_EButton_Grip;
        
        if (gripAction.GetState(handRight) && gripAction.GetState(handLeft))
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
