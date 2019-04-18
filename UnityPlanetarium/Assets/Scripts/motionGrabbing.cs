using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class motionGrabbing : MonoBehaviour
{
    Hand hand;

    public GameObject universe;

    Vector3 positionPre;
    Vector3 positionUniverse;
    public List<GameObject> Planets;

    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        hand = gameObject.GetComponent<Hand>();
        if (hand is null)
            Debug.LogError("No hand found.");
    }

    // Update is called once per frame
    void Update()
    {
        //axe bleu = z
        //axe vert = y
        //axe rouge = x
        if (SteamVR_Actions._default.GrabPinch.GetLastStateDown(hand.handType))
        {
            GameObject grabbingPlanet = Planets.Find(planet => planet.GetComponent<GrabCamera>()?.IsGrabbing ?? false);
            positionUniverse = grabbingPlanet?.transform.position ?? universe.transform.position;
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