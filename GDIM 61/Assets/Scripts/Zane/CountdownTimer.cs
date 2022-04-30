using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Written by Zane
public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float timeValue;
    [SerializeField] private TextMeshProUGUI timeText;

    public bool timeTicking;

    void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            timeTicking = true;
        }
        else
        {
            //send player to lose screen

            //stop timer at 0
            timeValue = 0;
            timeTicking = false;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeDisplay)
    {
        if (timeDisplay < 0)
        {
            timeDisplay = 0;
        }
        else if (timeDisplay > 0)
        {
            timeDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
