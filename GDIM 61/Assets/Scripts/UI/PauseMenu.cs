using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// handles the pause menu UI and what it does
public class PauseMenu : MonoBehaviour
{
    private bool GameIsPause = false; // if the game is paused or not

    [SerializeField]
    private GameObject pauseMenuUI; // UI of pause menu 

    //if press P or ESC, game is paused 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    //pause the game
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void LoadMenu()
    {
        GameIsPause = false;
        Time.timeScale = 1f;
        Debug.Log("Loading menu...");

        //goes back to menu
        SceneManager.LoadScene("Title");
    }

    //quits game in build
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
