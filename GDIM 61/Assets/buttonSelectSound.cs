using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonSelectSound : MonoBehaviour, ISelectHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect(BaseEventData eventData){
        //Debug.Log("playingsound");  
        AudioManager.instance.PlayOneShot("Hover");
        
    }

    public void playSelect()
    {
        AudioManager.instance.PlayOneShot("Select");
    }
}
