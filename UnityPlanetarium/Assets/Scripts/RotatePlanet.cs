using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    private static float DaysInYear = 365.25f;

    public float RotationPeriod = 1;

    private TimeManipulation _timeManipulationScript;
    public GameObject Globals;

    // Start is called before the first frame update
    void Start()
    {
        _timeManipulationScript = Globals.GetComponent<TimeManipulation>();
    }

    // Update is called once per frame
    void Update()
    {
        var dayPart = (_timeManipulationScript.Year * DaysInYear) % RotationPeriod / RotationPeriod;
        transform.localEulerAngles = new Vector3(0, 360f * dayPart, 0);
    }
}
