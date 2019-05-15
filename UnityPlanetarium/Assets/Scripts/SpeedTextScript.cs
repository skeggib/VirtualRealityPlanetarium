using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTextScript : MonoBehaviour
{
    public GameObject Globals;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yearsPerSecond_ = Globals.GetComponent<TimeManipulation>().YearsPerSecond;

        double yearsPerSecond = yearsPerSecond_;
        double daysPerSecond = yearsPerSecond * 365.25f;
        double hoursPerSecond = daysPerSecond * 24f;
        double minutesperSecond = hoursPerSecond * 60f;

        yearsPerSecond = System.Math.Round(yearsPerSecond, 2);
        daysPerSecond = System.Math.Round(daysPerSecond, 2);
        hoursPerSecond = System.Math.Round(hoursPerSecond, 2);
        minutesperSecond = System.Math.Round(minutesperSecond, 2);

        var str = string.Empty;
        if (yearsPerSecond > 1)
            str = $"{yearsPerSecond} years";
        else if (yearsPerSecond == 1)
            str = $"{yearsPerSecond} year";
        else if (daysPerSecond > 1)
            str = $"{daysPerSecond} days";
        else if (daysPerSecond == 1)
            str = $"{daysPerSecond} day";
        else if (hoursPerSecond > 1)
            str = $"{hoursPerSecond} hours";
        else if (hoursPerSecond == 1)
            str = $"{hoursPerSecond} hour";
        else if (minutesperSecond > 1)
            str = $"{minutesperSecond} minutes";
        else
            str = $"{System.Math.Round(minutesperSecond, 2)} minute";
        GetComponent<Text>().text = $"1 second = {str}";
    }
}
