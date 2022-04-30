using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject camera;
    private Transform image;
    private float normalXPos;
    private Image sprite;
    public float ratio = 1f;
    void Start()
    {
        camera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        normalXPos = transform.position.x;
        LookAt image = this.gameObject.transform.GetChild(0).GetComponent<LookAt>();
        image.camera = camera;
        image.target = target;
        sprite = this.gameObject.transform.GetChild(0).GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = target.transform.position - camera.transform.position;

        //For testing color
        //updateColor(ratio);
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

    public void updateColor(float ratio)
    {
        if(ratio <= 0.5f )
        {
            sprite.color = new Color(1f,ratio*2,0f,1f);
            
        }
        else if (ratio <= 1f)
        {
            sprite.color = new Color(1f,1f,0.5f + 0.5f*ratio,1f);
        }
    }
}
