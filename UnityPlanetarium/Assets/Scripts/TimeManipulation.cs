using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TimeManipulation : MonoBehaviour
{
    public float YearsPerSecond = 0.1f;

    public float Year;

    private bool _pause;
    private bool _pausePressed;


    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P) && !_pausePressed)
        {
            _pausePressed = true;
            _pause = !_pause;
        }
        else if (!Input.GetKey(KeyCode.P))
        {
            _pausePressed = false;
        }

        if (!_pause)
        {
            if (Input.GetKey(KeyCode.G))
                YearsPerSecond *= 0.99f;
            if (Input.GetKey(KeyCode.H))
                YearsPerSecond *= 1.01f;

            if (YearsPerSecond < 0.01)
                YearsPerSecond = 0.01f;
            else if (YearsPerSecond > 1)
                YearsPerSecond = 1f;

            var delta = Time.deltaTime; // seconds
            var elapsedTime = delta * YearsPerSecond;
            Year += elapsedTime;
        }
    }
}
