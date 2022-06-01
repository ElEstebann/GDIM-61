using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class AIAnimControllerAccess : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private List<string> paramNames;
    [SerializeField] private int paramIndex;
    [SerializeField] private bool isTrigger;
    [SerializeField] private bool boolVal;
    void Start()
    {
        if (anim == null)
        {
            print("Error, animator controller not found");
        }
    }

    private void Update()
    {
        if (paramIndex >= 0)
        {
            if (isTrigger)
            {
                anim.SetTrigger(paramNames[paramIndex]);
            }
            else
            {
                anim.SetBool(paramNames[paramIndex], boolVal);
            }
        }
    }
}
