using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Written by Amber/Zane
public class PauseMenuScreen : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // UI of pause menu 
    [SerializeField] private GameObject startMenuUI; //For tutorial messages
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button startMenuResumeButton;

    private static bool tutorialTwoDone = false;

    private bool gamePaused;

    public bool specialStart; //For displaying message on start

    public void Start()
    {   
        if(specialStart && !tutorialTwoDone)
        {
            StartCoroutine(SpecialPause());
            tutorialTwoDone = true;
        }
    }

    void Update()
    {
        // if player presses Escape then the game is paused or resumed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    // pauses the game
    public void Pause()
    {
        GameManager.PauseGame();
        pauseMenuUI.SetActive(true);
        resumeButton.Select();
        gamePaused = true;
    }

    IEnumerator SpecialPause() //For displaying special message on start
    {
        yield return new WaitForSeconds(1);
        GameManager.PauseGame();
        startMenuUI.SetActive(true);
        startMenuResumeButton.Select();
        gamePaused = true;
    }

    // resumes the game
    public void Resume()
    {
        GameManager.ResumeGame();
        pauseMenuUI.SetActive(false);
        gamePaused = false;
    }

    public void SpecialResume()
    {
        GameManager.ResumeGame();
        startMenuUI.SetActive(false);
        gamePaused = false;
    }

    // restarts the game
    public void Restart()
    {
        GameManager.NewGame();
    }

    // quits to the main menu
    public void QuitToMenu()
    {
        Time.timeScale = 1.0f;
        gamePaused = false;
        
        GameManager.QuitToMenu();
        
    }

    // quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
