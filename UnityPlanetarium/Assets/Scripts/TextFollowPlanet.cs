using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollowPlanet : MonoBehaviour
{
    public GameObject Globals;
    public GameObject Planet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var camera = Globals.GetComponent<Globals>().Camera;
        var behind = Vector3.Dot((Planet.transform.position - camera.transform.position), camera.transform.forward) < 0;
        Debug.Log($"{Planet.name} {behind}");

        var position = camera.WorldToScreenPoint(Planet.transform.position);
        position.x += GetComponent<RectTransform>().sizeDelta.x / 2f;
        position.y += GetComponent<RectTransform>().sizeDelta.y / 2f;
        transform.position = position;

        if (behind)
            GetComponent<CanvasRenderer>().SetAlpha(0);
        else
            GetComponent<CanvasRenderer>().SetAlpha(1);
    }
}
