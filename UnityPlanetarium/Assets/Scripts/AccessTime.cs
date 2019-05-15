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
        if (SteamVR_Actions._default.touchpadPress.GetStateDown(hand.handType))
        {
            Debug.Log("TrackPad clicked");
            Debug.Log(getTrackpadPosition().x);
            if (getTrackpadPosition().x > 0)
            {
                Debug.Log("Plus");
                _globalsScript.GetComponent<TimeManipulation>().YearsPerSecond *= 1.01f;
            }
            else
            {
                Debug.Log("Moins");
                _globalsScript.GetComponent<TimeManipulation>().YearsPerSecond *= 0.99f;
            }
        }
    }
}
