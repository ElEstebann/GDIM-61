using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Written by Zane
public class EndScreen : MonoBehaviour
{
    [SerializeField] public bool win;
    [SerializeField] private TextMeshProUGUI loseText;

    void Start()
    {
        if(win)
        {
            AudioManager.instance.Play("Win");
        }
        else
        {
            loseText.text = GhostTask.loseMessageText;
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
