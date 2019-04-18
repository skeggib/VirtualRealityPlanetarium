using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    private static float AstronomicalUnit = 149597870.700f;

    public float SizeKm;

    // Start is called before the first frame update
    void Start()
    {
        var scale = SizeKm / AstronomicalUnit * 1000;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
