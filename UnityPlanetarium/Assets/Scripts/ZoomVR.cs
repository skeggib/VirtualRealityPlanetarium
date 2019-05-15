using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ZoomVR : MonoBehaviour
{
    Hand hand;

    bool zooming = false;

    public GameObject Globals;
    private Globals _globalsScript;

    public float MaxScale = 5000;
    public float MinScale = 0.1f;

    private float _startRange;

    // Start is called before the first frame update
    void Start()
    {
        _globalsScript = Globals.GetComponent<Globals>();
        _startRange = _globalsScript.Sun.GetComponent<Light>().range / _globalsScript.World.transform.localScale.x;

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
        GameObject grabbingPlanet = _globalsScript.Planets.Find(planet => planet.GetComponent<GrabCamera>()?.IsGrabbing ?? false);

        var pivot = transform.position;//grabbingPlanet?.transform.position ?? _globalsScript.World.transform.position;

        var one = new Vector3(1, 1, 1);
        var scale = Time.deltaTime * 3;
        var mult = new Vector3(scale, scale, scale);

        if (SteamVR_Actions._default.touchpadPress.GetStateDown(hand.handType))
        {
            Debug.Log("Right TrackPad clicked");
            
            zooming = true;

            _globalsScript.Sun.GetComponent<Light>().range = _startRange * _globalsScript.World.transform.localScale.x;
        }
        if (SteamVR_Actions._default.touchpadPress.GetStateUp(hand.handType))
        {
            zooming = false;
        }
        if(zooming)
        {
            if (getTrackpadPosition().y > 0)
            {
                Debug.Log("Plus");
                ScaleAround(_globalsScript.World.transform, pivot, one + mult);

                if (transform.localScale.x > MaxScale)
                    ScaleAround(_globalsScript.World.transform, pivot, new Vector3(MaxScale / _globalsScript.World.transform.localScale.x, MaxScale / _globalsScript.World.transform.localScale.y, MaxScale / _globalsScript.World.transform.localScale.z));
            }
            else
            {
                Debug.Log("Moins");
                ScaleAround(_globalsScript.World.transform, pivot, one - mult);

                if (transform.localScale.x < MinScale)
                    ScaleAround(_globalsScript.World.transform, pivot, new Vector3(MinScale / _globalsScript.World.transform.localScale.x, MinScale / _globalsScript.World.transform.localScale.y, MinScale / _globalsScript.World.transform.localScale.z));
            }
        }
    }

    public static void ScaleAround(Transform target, Vector3 pivot, Vector3 scale)
    {
        var targetParent = target.parent;
        var empty = new GameObject();
        empty.transform.Translate(pivot);
        target.parent = empty.transform;
        var temp = empty.transform.localScale;
        temp.Scale(scale);
        empty.transform.localScale = temp;
        target.parent = targetParent;
        Destroy(empty);
    }
}
