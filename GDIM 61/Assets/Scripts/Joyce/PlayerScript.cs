using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float xtrans;
    private float ytrans;
    [SerializeField] private Animator anim;

    private bool faceRight;

    void Start()
    {
        anim = this.GetComponent<Animator>();

        faceRight = true;
    }

    void Update()
    {
        xtrans = Input.GetAxis("Horizontal") * speed;
        if (xtrans > 0) // determines which way the player is facing
        {
            faceRight = true;
        }
        else if (xtrans < 0)
        {
            faceRight = false;
        }
        ytrans = Input.GetAxis("Vertical") * speed;

        anim.SetFloat("Speed", new Vector2(xtrans, ytrans).magnitude);
        //print(anim.GetFloat("Speed"));
        anim.SetBool("FaceRight", faceRight);
    }
    private void FixedUpdate()
    {
        transform.Translate(xtrans * Time.fixedDeltaTime, ytrans * Time.fixedDeltaTime , 0);
    }

}
