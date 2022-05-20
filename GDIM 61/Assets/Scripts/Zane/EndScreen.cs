using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Written by Zane
public class EndScreen : MonoBehaviour
{
    [SerializeField] public bool win;
    void Start()
    {
        if(win)
        {
            AudioManager.instance.Play("Win");
        }
        else
        {
            AudioManager.instance.Play("Lose");
        }
    }
    // quits to the main menu
    public void QuitToMenu()
    {
        GameManager.QuitToMenu();
    }

    // restarts the game
    public void Restart()
    {
        GameManager.NewGame();
    }
}
