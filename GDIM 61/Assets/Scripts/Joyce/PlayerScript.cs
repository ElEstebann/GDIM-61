using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Joyce Mai
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float xtrans;
    private float ytrans;
    [SerializeField] private Animator anim; // reference to animator; controls the left/right idle/movement animation trasitions

    private bool faceRight;

    void Start()
    {
        anim = this.GetComponent<Animator>();

        faceRight = true;
    }

    void Update()
    {
        xtrans = Input.GetAxis("Horizontal") * speed;
        ytrans = Input.GetAxis("Vertical") * speed;

        if (xtrans > 0) // determines which way the player is facing
        {
            faceRight = true;
        }
        else if (xtrans < 0)
        {
            faceRight = false;
        }
        // variables that determine animation state
        anim.SetFloat("Speed", new Vector2(xtrans, ytrans).magnitude);
        anim.SetBool("FaceRight", faceRight);
    }
    private void FixedUpdate() // using fixed update instead of Update to decrease jitter
    {
        transform.Translate(xtrans * Time.fixedDeltaTime, ytrans * Time.fixedDeltaTime , 0);
    }

}
