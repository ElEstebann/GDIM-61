using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FearBar : MonoBehaviour
{
    [SerializeField] GameObject ai;
    private HumanFOV detection;

    [SerializeField] private float scale = 0.001f;
    private float max = 1f;
    private float min = 0f;
    private float newX;
    
    void Start()
    {
        detection = ai.GetComponent<HumanFOV>();
    }

    void Update()
    {
        FillBar();
    }

    void FillBar()
    {
        if (detection.detectPlayer)
        {
            if (transform.localScale.x <= max)
            {
                transform.localScale = new Vector3(transform.localScale.x + scale, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                //transform.localScale = new Vector3(max, transform.localScale.y, transform.localScale.z);
                GameManager.LoseScreen();
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

}
