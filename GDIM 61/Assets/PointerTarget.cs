using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerObject : MonoBehaviour
{
    private Collider2D cameraCheck;
    [SerializeField] public GameObject pointerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        cameraCheck = GetComponent<Collider2D>();
        Debug.Log("Ready");
        if(true)
        {
            GameObject canvas = GameObject.Find("Overlay");
            Instantiate(pointerPrefab,canvas.transform);
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
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.tag == "MainCamera")
        {
            Debug.Log(collision.tag + " left");
        }
        
        
    }
}
