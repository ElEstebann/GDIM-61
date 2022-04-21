using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target && camera)
        {
            //Rotate by angle from camera to target
            //Set Z to 0 because its 2d
            transform.right = target.transform.position - camera.transform.position;
            transform.right = new Vector3(transform.right.x,transform.right.y,0);    
        }
    }

}
