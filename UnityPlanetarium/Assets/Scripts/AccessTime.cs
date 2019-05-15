using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class AccessTime : MonoBehaviour
{
    Hand hand;
    public GameObject Globals;
    private Globals _globalsScript;
    private bool _pressed;

    // Start is called before the first frame update
    void Start()
    {
        _globalsScript = Globals.GetComponent<Globals>();
        hand = gameObject.GetComponent<Hand>();
        if (hand is null)
            Debug.LogError("No hand found.");
    }

    public Vector2 getTrackpadPosition()
    {
        SteamVR_Action_Vector2 trackpadPos = SteamVR_Actions._default.touchpadPos;
        return trackpadPos.GetAxis(hand.handType);

    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions._default.GrabPinch.GetLastStateDown(hand.handType)) {
            _globalsScript.GetComponent<TimeManipulation>()._pause = !_globalsScript.GetComponent<TimeManipulation>()._pause;
        }
        if (!_globalsScript.GetComponent<TimeManipulation>()._pause)
        {
            if (SteamVR_Actions._default.touchpadPress.GetStateDown(hand.handType))
            {
                _pressed = true;

            }
            if (SteamVR_Actions._default.touchpadPress.GetStateUp(hand.handType))
            {
                _pressed = false;
            }
            if (_pressed)
            {
                if (getTrackpadPosition().x > 0)
                {
                    Debug.Log("plusgauche");
                    _globalsScript.GetComponent<TimeManipulation>().YearsPerSecond *= 1.01f;
                }
                else
                {
                    Debug.Log("Moinsgauche");
                    _globalsScript.GetComponent<TimeManipulation>().YearsPerSecond *= 0.99f;
                }
            }
        }
    }
}
