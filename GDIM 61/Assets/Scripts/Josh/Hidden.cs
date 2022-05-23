using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{
    [SerializeField] private GameObject fearbar;
    private FearBar fear;
    [SerializeField] private SpriteRenderer meep;

    [SerializeField] private float fillRate = 0.15f;
    [SerializeField] private float depleteRate = 0.3f;
    private float max = 1f;
    private float min = 0f;
    private float ogVal = 1f;
    private float hideVal = 0.5f;
    void Start()
    {
        fear = fearbar.GetComponent<FearBar>();
        transform.localScale = new Vector3(max, transform.localScale.y, transform.localScale.z);
    }

    
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if ((!fear.hidden) && (transform.localScale.x == max))
            {
                fear.hidden = true;
                meep.color = new Color(ogVal, ogVal, ogVal, hideVal);
            }
        }

        UpdateSkillBar();
    }

    void UpdateSkillBar()
    {
        if (fear.hidden)
        {
            if (transform.localScale.x > min)
            {
                transform.localScale = new Vector3(transform.localScale.x - (depleteRate * Time.deltaTime), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                fear.hidden = false;
                meep.color = new Color(ogVal, ogVal, ogVal, ogVal);
            }
        }

        else
        {
            if (transform.localScale.x < max)
            {
                transform.localScale = new Vector3(transform.localScale.x + (fillRate * Time.deltaTime), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(max, transform.localScale.y, transform.localScale.z);
            }
        }
        
    }

}
