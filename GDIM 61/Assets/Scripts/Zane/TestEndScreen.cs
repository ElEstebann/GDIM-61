using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestEndScreen : MonoBehaviour
{
    public void QuitToMenu()
    {
        GameManager.QuitToMenu();
    }

    public void Restart()
    {
        GameManager.NewGame();
    }
}
