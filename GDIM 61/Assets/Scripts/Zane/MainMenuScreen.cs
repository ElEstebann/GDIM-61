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

    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject creditsScreen;

    private void Start()
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

    public void ActivateTutorial()
    {
        Debug.Log("Tutorial");
        titleAnimator.SetTrigger("Change");
        titleAnimator.SetBool("Tutorial", true);
    }
    

    // credits appear
    public void ActivateCredits()
    {
        Debug.Log("Credits");
        //titleAnimator.SetTrigger("Change");
        //titleAnimator.SetBool("Credits", true);
        mainScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    // credits disappear
    public void DectivateCredits()
    {
        //titleAnimator.ResetTrigger("Change");
        //titleAnimator.SetBool("Credits", false);
        creditsScreen.SetActive(false);
        mainScreen.SetActive(true);
        
    }

    // options appear
    public void ActivateOptions()
    {
        Debug.Log("Options");
        mainScreen.SetActive(false);
        optionsScreen.SetActive(true);
        //titleAnimator.SetTrigger("Change");
        //titleAnimator.SetBool("Options", true);
    }

    // options disappear
    public void DectivateOptions()
    {
        //titleAnimator.ResetTrigger("Change");
        //titleAnimator.SetBool("Options", false);
        optionsScreen.SetActive(false);
        mainScreen.SetActive(true);
        
    }

    // quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
