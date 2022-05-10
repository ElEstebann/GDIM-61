using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTitleScreen : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Awake()
    {
        GameManager.TitleScreen();
        AudioManager.instance.Play("MenuTheme");
    }

    public void ActivateStart()
    {
        //GameManager.NewGame();
        AudioManager.instance.Stop("MenuTheme");

    }

    public void ActivateCredits()
    {
        anim.SetTrigger("Change");
        anim.SetBool("Credits", true);

    }

    public void DectivateCredits()
    {
        anim.SetBool("Credits", false);
    }

    public void ActivateOptions()
    {
        anim.SetTrigger("Change");
        anim.SetBool("Options", true);
    }

    public void DectivateOptions()
    {
        anim.SetBool("Options", false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
