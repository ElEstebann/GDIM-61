using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Collider2D cameraCheck;
    [SerializeField] public GameObject pointerPrefab;
    [SerializeField] public Sprite targetSprite;
    public GameObject pointer;
    public Arrow arrow;
   

    // Start is called before the first frame update
    void Awake()
    {
        cameraCheck = GetComponent<Collider2D>();
        
        //Instantiate pointer prefab on Overlay canvas
        GameObject canvas = GameObject.Find("Overlay");
        pointer = Instantiate(pointerPrefab,canvas.transform);
        arrow = pointer.GetComponent<Arrow>();
        arrow.target = gameObject;
        arrow.targetSprite = targetSprite;
            

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When target is in view of camera, enable pointer
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "MainCamera")
        {
            //Debug.Log(collision.tag + " can see object");
            disablePointer();
        }
        
    }
    //When target is no longer in camera view, diable pointer
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.tag == "MainCamera")
        {
            //Debug.Log(collision.tag + " left");
            enablePointer();
        }
        
        
    }

    public void disablePointer()
    {
        arrow.disable();
    }

    public void enablePointer()
    {
        arrow.enable();
    }

    public void updatePointerColor(float ratio)
    {
        arrow.updateProgress((float)ratio);
    }

    void OnDestroy()
    {
        if(arrow)
        {
            arrow.destroy();
        }
    }

}
