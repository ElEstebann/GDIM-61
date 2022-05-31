using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimControllerAccess : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] bool walking;
    void Start()
    {
        if (anim == null)
        {
            print("Error, animator controller not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Walking", walking);
    }
}
