using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Written by Zane
public class EndScreen : MonoBehaviour
{
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
