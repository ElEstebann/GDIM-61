using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Written by Zane
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<string> levels = new List<string>();
    [SerializeField] private string SceneName;
    private int level = 1;

    private static GameManager instance;

    enum GAMESTATE
    {
        TITLESCREEN,
        PLAYING,
        PAUSED,
        WIN,
        LOSE
    }

    private static GAMESTATE state;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this);
        }
    }

    public static void TitleScreen()
    {
        state = GAMESTATE.TITLESCREEN;
    }

    public static void NewGame()
    {
        state = GAMESTATE.PLAYING;
        instance.level = 0;
        SceneManager.LoadScene(instance.levels[0]);
        Time.timeScale = 1.0f;
    }

    public static void NextLevel()
    {
        state = GAMESTATE.PLAYING;
        SceneManager.LoadScene(instance.levels[instance.level++]);
    }

    public static void PauseGame()
    {
        state = GAMESTATE.PAUSED;
    }

    public static void ResumeGame()
    {
        state = GAMESTATE.PLAYING;
    }

    public static void QuitToMenu()
    {
        state = GAMESTATE.TITLESCREEN;
        instance.level = 1;
        SceneManager.LoadScene(0);
    }

    public static void WinGame()
    {
        state = GAMESTATE.WIN;
    }

    public static void LoseGame()
    {
        state = GAMESTATE.LOSE;
    }
}
