using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Written by Zane
public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float timeValue;
    [SerializeField] private TextMeshProUGUI timeText;

    private int sceneIndex;

    public bool timeTicking;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneIndex = currentScene.buildIndex;

        Debug.Log(sceneIndex);
        Debug.Log(SceneManager.sceneCountInBuildSettings);
    }

    void Update()
    {
        if(timeValue > 0)
        {
            timeTicking = true;

            // timer counts down
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeTicking = false;

            // timer stops at zero
            timeValue = 0;

            // checks if it is the last level
            if (sceneIndex == SceneManager.sceneCountInBuildSettings - 3)
            {
                GameManager.WinScreen();
            }
            else
            {
                GameManager.NextLevel();
            }
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
