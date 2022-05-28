using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Written by Amber/Zane
public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private Animator titleAnimator;
    [SerializeField] private Image optionsIcon;
    [SerializeField] private Image creditsIcon;

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
        Debug.Log("Credits");
        titleAnimator.SetTrigger("Change");
        titleAnimator.SetBool("Credits", true);
    }

    // credits disappear
    public void DectivateCredits()
    {
        titleAnimator.ResetTrigger("Change");
        titleAnimator.SetBool("Credits", false);
    }

    // options appear
    public void ActivateOptions()
    {
        Debug.Log("Options");
        titleAnimator.SetTrigger("Change");
        titleAnimator.SetBool("Options", true);
    }

    // options disappear
    public void DectivateOptions()
    {
        titleAnimator.ResetTrigger("Change");
        titleAnimator.SetBool("Options", false);
    }

    // quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
