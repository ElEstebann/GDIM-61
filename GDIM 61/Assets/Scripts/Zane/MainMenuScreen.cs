using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Written by Amber/Zane
public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private Animator titleAnimator;

    private void Awake()
    {
        GameManager.TitleScreen();
        AudioManager.instance.Play("MenuTheme");
    }

    // starts the game
    public void ActivateStart()
    {
        GameManager.NewGame();
        AudioManager.instance.Stop("MenuTheme");
    }

    // credits appear
    public void ActivateCredits()
    {
        titleAnimator.SetTrigger("Change");
        titleAnimator.SetBool("Credits", true);

    }

    // credits disappear
    public void DectivateCredits()
    {
        titleAnimator.SetBool("Credits", false);
    }

    // options appear
    public void ActivateOptions()
    {
        titleAnimator.SetTrigger("Change");
        titleAnimator.SetBool("Options", true);
    }

    // options disappear
    public void DectivateOptions()
    {
        titleAnimator.SetBool("Options", false);
    }

    // quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
