using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject camera;
    private Transform image;
    private float normalXPos;
    void Start()
    {
        camera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        normalXPos = transform.position.x;
        LookAt image = this.gameObject.transform.GetChild(0).GetComponent<LookAt>();
        image.camera = camera;
        image.target = target;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = target.transform.position - camera.transform.position;
        
        //These values found by trial and error lol
        float xMax = 860f;
        float yMax = 440f;
        //Transform the position of the arrow to the closest edge of the screen & keep them on screen boarders
        //Keep Arrow
        if(Mathf.Abs(transform.localPosition.x)/xMax > Mathf.Abs(transform.localPosition.y)/yMax)
        {
            transform.localPosition *= xMax/Mathf.Abs(transform.localPosition.x);
            if (Mathf.Abs(transform.localPosition.y) > yMax)
            {
                transform.localPosition = new Vector3(transform.localPosition.x,yMax*Mathf.Sign(transform.localPosition.y),transform.localPosition.z);
            }
        }
        else
        {
            transform.localPosition *= yMax/Mathf.Abs(transform.localPosition.y);
            if(Mathf.Abs(transform.localPosition.x) > xMax)
            {
                transform.localPosition = new Vector3(transform.localPosition.x,yMax,transform.localPosition.z);
            }
        }
        //Debug.Log("x: " + transform.localPosition.x + "y: " +transform.localPosition.y);
    }
}
