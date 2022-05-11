using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Joyce Mai
public class MovementScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float xtrans;
    private float ytrans;
    [SerializeField] private Animator anim; // reference to animator; controls the left/right idle/movement animation trasitions
    [SerializeField] private GameObject cam; // referece to the main camera which will be used to tracking purposes later
    private CameraScript camScript;
    [SerializeField] private string CamTag;

    private bool faceRight;
    [SerializeField] private bool freeze;

    void Start()
    {
        anim = this.GetComponent<Animator>();

        faceRight = true;
        freeze = false;
        camScript = cam.GetComponent<CameraScript>();
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
        if(xtrans != 0 || ytrans != 0)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
        anim.SetBool("FaceRight", faceRight);

        // example code to set the triggers for the fixing animation - added functions to trigger these externally
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Fix");
        }
        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("Fixing", true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool("Fixing", false);
        }
        */
    }
    private void FixedUpdate() // using fixed update instead of Update to decrease jitter
    {
        if (!freeze) // only let the player move if the player is not fixing something // this variable is set in the animation state
        {
            transform.Translate(xtrans * Time.fixedDeltaTime, ytrans * Time.fixedDeltaTime, 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(CamTag) && camScript != null)
        {
            camScript.SnapCam(collision.gameObject.transform);
        }
    }

    public void SetFixTrigger()
    {
        anim.SetTrigger("Fix");
    }
    public void SetFixingBool(bool isFixing)
    {
        anim.SetBool("Fixing", isFixing);
    }


}
