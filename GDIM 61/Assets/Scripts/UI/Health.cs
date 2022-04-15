using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject[] hearts;

    [SerializeField]
    private int life;

    private bool dead;

    void Start()
    {
        life = hearts.Length;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)) //change to if being seen 
        {
            TakeDamage(1);
        }

        if(dead == true)
        {
            Debug.Log("dead!");
            //game ended
        }
    }

    public void TakeDamage(int d)
    {
        if(life >= 1)
        {
            life -= d;
            Destroy(hearts[life].gameObject);
            if (life < 1)
            {
                dead = true;
            }
        }
    }
}
