using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollowPlanet : MonoBehaviour
{
    public Camera Camera;
    public GameObject Planet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position = Camera.WorldToScreenPoint(Planet.transform.position);
        position.x += GetComponent<RectTransform>().sizeDelta.x / 2f;
        position.y += GetComponent<RectTransform>().sizeDelta.y / 2f;
        transform.position = position;
    }
}
