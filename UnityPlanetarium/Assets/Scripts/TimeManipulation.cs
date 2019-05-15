using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TimeManipulation : MonoBehaviour
{
    public float YearsPerSecond = 1f / 365.25f * 10f;
    private float _min = 1f / 365.25f / 24f;
    private float _max = 1;

    public float Year;

    public bool _pause;
    public bool _pausePressed;


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

            if (YearsPerSecond < _min)
                YearsPerSecond = _min;
            else if (YearsPerSecond > _max)
                YearsPerSecond = _max;

            var delta = Time.deltaTime; // seconds
            var elapsedTime = delta * YearsPerSecond;
            Year += elapsedTime;
        }
    }
}
