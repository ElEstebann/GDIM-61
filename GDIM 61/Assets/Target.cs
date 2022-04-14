using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Collider2D cameraCheck;
    [SerializeField] public GameObject pointerPrefab;
    public GameObject pointer;
    public Arrow arrow;
   

    // Start is called before the first frame update
    void Start()
    {
        cameraCheck = GetComponent<Collider2D>();
        Debug.Log("Ready");
        if(true)
        {
            GameObject canvas = GameObject.Find("Overlay");
            pointer = Instantiate(pointerPrefab,canvas.transform);
            arrow = pointer.GetComponent<Arrow>();
            arrow.target = gameObject;
            

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "MainCamera")
        {
            Debug.Log(collision.tag + " can see object");
            pointer.SetActive(false);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.tag == "MainCamera")
        {
            Debug.Log(collision.tag + " left");
            pointer.SetActive(true);
        }
        
        
    }
}
