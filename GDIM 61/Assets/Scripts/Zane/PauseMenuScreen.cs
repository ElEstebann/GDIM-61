using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Written by Amber/Zane
public class PauseMenuScreen : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // UI of pause menu 

    private bool gamePaused;

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
        gamePaused = true;
    }

    // resumes the game
    public void Resume()
    {
        GameManager.ResumeGame();
        pauseMenuUI.SetActive(false);
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
        AudioManager.instance.Stop("MainTheme");
        GameManager.QuitToMenu();
        gamePaused = false;
    }

    // quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
