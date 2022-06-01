using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FearBar : MonoBehaviour
{
    [SerializeField] GameObject[] ai;
    private List<HumanFOV> detection = new List<HumanFOV>();

    

    [SerializeField] private float scale = 0.15f;

    [SerializeField] private Animator Transition;

    private float initialFill = 0f;
    private float max = 1f;
    public bool hidden = false;
    
    
    void Start()
    {
        transform.localScale = new Vector3(initialFill, transform.localScale.y, transform.localScale.z);
        for (int i = 0; i < ai.Length; i++)
        {
            detection.Add(ai[i].GetComponent<HumanFOV>());
        }
    }

    void Update()
    {
        for (int i = 0; i < detection.Count; i++)
        {
            FillBar(detection[i]);
        }
    }

    void FillBar(HumanFOV detection)
    {
        if (detection.detectPlayer && !hidden)
        {
            if (transform.localScale.x <= max)
            {
                transform.localScale = new Vector3(transform.localScale.x + (scale * Time.deltaTime), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                //transform.localScale = new Vector3(max, transform.localScale.y, transform.localScale.z);
                //GameManager.LoseScreen();
                StartCoroutine(transition());
            }
            
        }
       // else
       // {
       //     if (transform.localScale.x >= min)
       //     {
       //         transform.localScale = new Vector3(transform.localScale.x - scale, transform.localScale.y, transform.localScale.z);
       //     }
       //     else
       //     {
       //         transform.localScale = new Vector3(min, transform.localScale.y, transform.localScale.z);
       //     }

       // }
    }

    IEnumerator transition ()
    {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        //SceneManager.LoadScene(LevelIndex);
        GameManager.LoseScreen();
    }

}
