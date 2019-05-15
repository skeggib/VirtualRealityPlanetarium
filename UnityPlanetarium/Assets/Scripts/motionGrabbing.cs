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

    Vector3 _controllerStartPosition;
    Vector3 _initialUniversePosition;
    Vector3 _lastUniversePosition;

    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        _globalsScript = Globals.GetComponent<Globals>();
        hand = gameObject.GetComponent<Hand>();
        if (hand is null)
            Debug.LogError("No hand found.");
        _lastUniversePosition = universe.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        var controllerCurrentPosition = transform.position;
        var grabbingPlanet = _globalsScript.Planets.Find(planet => planet.GetComponent<GrabCamera>()?.IsGrabbing ?? false);

        if (SteamVR_Actions._default.GrabPinch.GetLastStateDown(hand.handType))
        {
            _initialUniversePosition = universe.transform.position;
            _controllerStartPosition = new Vector3(controllerCurrentPosition.x, controllerCurrentPosition.y, controllerCurrentPosition.z);

            moving = true;
        }

        if (SteamVR_Actions._default.GrabPinch.GetStateUp(hand.handType))
        {
            moving = false;
        }

        if (!(grabbingPlanet is null))
        {
            _initialUniversePosition += universe.transform.position - _lastUniversePosition;
        }

        if (moving)
        {
            var move = controllerCurrentPosition - _controllerStartPosition;
            universe.transform.position = _initialUniversePosition + move;
        }
            
        _lastUniversePosition = universe.transform.position;
    }
}