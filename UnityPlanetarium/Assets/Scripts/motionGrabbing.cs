using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class motionGrabbing : MonoBehaviour
{
    Hand hand;

    public GameObject universe;

    public GameObject Globals;
    private Globals _globalsScript;

    Vector3 positionPre;
    Vector3 positionUniverse;

    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        _globalsScript = Globals.GetComponent<Globals>();
        hand = gameObject.GetComponent<Hand>();
        if (hand is null)
            Debug.LogError("No hand found.");
    }


    // Update is called once per frame
    void Update()
    {

        if (SteamVR_Actions._default.GrabPinch.GetLastStateDown(hand.handType))
        {
            GameObject grabbingPlanet = _globalsScript.Planets.Find(planet => planet.GetComponent<GrabCamera>()?.IsGrabbing ?? false);
            positionUniverse =  universe.transform.position;
            
            positionPre = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            moving = true;
        }
        if (SteamVR_Actions._default.GrabPinch.GetStateUp(hand.handType))
        {
            moving = false;
        }

        if (moving)
        {
            Vector3 move = transform.position - positionPre;

            universe.transform.position = positionUniverse + move;
        }
    }
}