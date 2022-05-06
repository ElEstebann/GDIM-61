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
            timeTicking = true;

            // timer counts down
            timeValue -= Time.deltaTime;

            // tasks start to spawn
            // GameObject.Find("Task Handler").GetComponent<GhostTaskSpawner>().SpawnTask();
        }
        else
        {
            timeTicking = false;

            // timer stops at zero
            timeValue = 0;

            // sends player to next level
            //GameManager.NextLevel();

            GameManager.WinScreen();
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
