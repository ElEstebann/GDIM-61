using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    public void ActivateCredits()
    {
        anim.SetTrigger("Change");
        anim.SetBool("Credits",true);
        
    }

    public void DectivateCredits()
    {
        anim.SetBool("Credits",false);
    }

    public void ActivateOptions()
    {
        anim.SetTrigger("Change");
        anim.SetBool("Options",true);
    }

    public void DectivateOptions()
    {
        anim.SetBool("Options",false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
