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

    Vector3 initialCenterControllerPosition;
    float initialDistanceControllers;
    Vector3 initialScale;

    Vector3 initialPosition;

    public List<GameObject> Planets;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (handRight.grabGripAction.GetState(handRightsource) && handLeft.grabGripAction.GetState(handLeftsource) && !zooming)
        {
            GameObject grabbingPlanet = Planets.Find(planet => planet.GetComponent<GrabCamera>()?.IsGrabbing ?? false);

            initialScale = transform.localScale;
            initialPosition = grabbingPlanet?.transform.position ?? transform.position;
            initialDistanceControllers = Vector3.Distance(handLeft.transform.localPosition, handRight.transform.localPosition);
            
            initialCenterControllerPosition = Vector3.Lerp(handLeft.transform.position, handRight.transform.position, 0.5f);
            
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

        Vector3 Pi = initialPosition;
        Vector3 Ps = initialCenterControllerPosition;

        //transform.position = new Vector3(Pi.x + (Pi - Ps).normalized.x * scale, Pi.y + (Pi - Ps).normalized.y * scale, Pi.z + (Pi - Ps).normalized.z * scale);

        //transform.position = Pi + Ps - new Vector3((Pi.x - Ps.x) * scale, (Pi.y - Ps.y) * scale, (Pi.z - Ps.z) * scale);
        //transform.position = initialPosition - new Vector3((initialCenterControllerPosition.x - initialPosition.x) * scale, (initialCenterControllerPosition.y - initialPosition.y) * scale, (initialCenterControllerPosition.z - initialPosition.z) * scale);
    }
}
