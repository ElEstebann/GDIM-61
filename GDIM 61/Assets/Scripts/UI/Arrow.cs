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
    private Image targetImage;
    public float progress = 0.5f;
    public bool active = true;
    public Sprite targetSprite = null;
    void Start()
    {
        camera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        normalXPos = transform.position.x;
        LookAt image = this.gameObject.transform.GetChild(0).GetComponent<LookAt>();
        image.camera = camera;
        image.target = target;

        LookAt objectSprite = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<LookAt>();
        objectSprite.camera = camera;
        objectSprite.target = target;
        targetImage = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        if(targetSprite)
        {
            targetImage.sprite = targetSprite;
        }
        else{
            Debug.Log("BTW u forgot to set targetSprite on Target object");
        }

        sprite = this.gameObject.transform.GetChild(0).GetComponent<Image>();
        pointPosition();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target.activeSelf)
        {

            
            
            pointPosition();
            updateColor(progress);
        }
        else
        {
            beInvisible();
        }
        //Debug.Log("x: " + transform.localPosition.x + "y: " +transform.localPosition.y);
    }

    private void pointPosition()
    {
        transform.localPosition = target.transform.position - camera.transform.position;
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
    }
    public void updateColor(float ratio)
    {
        if(active && sprite)
        {
            if(ratio <= 0.5f )
            {
                sprite.color = new Color(1f,ratio*2,0f,1f);
                
            }
            else if (ratio <= 1f)
            {
                sprite.color = new Color(1f,1f,2f*(ratio-0.5f),1f);
            }
            targetImage.color = new Color(1f,1f,1f,1f);
        }
    }

    public void beInvisible()
    {
        if(sprite){
            sprite.color = new Color(sprite.color.r,sprite.color.b,sprite.color.g,0f);
            targetImage.color = new Color(1f,1f,1f,0f);
            
        }
    }

    public void beVisible()
    {
        updateColor(progress);
    }

    public void disable()
    {
        active = false;
        beInvisible();
    }

    public void enable()
    {
        active = true;
        pointPosition();
        updateColor(progress);
        
    }

    public void updateProgress(float ratio)
    {
        progress = ratio;
    }

    

}
