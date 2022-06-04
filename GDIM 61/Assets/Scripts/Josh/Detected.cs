using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour
{
    [SerializeField] GameObject ai;
    [SerializeField] private GameObject alert;
    private HumanFOV detection;
    [SerializeField] private GameObject fearbar;
    private FearBar fear;
    private bool isdetected = false;
    // [SerializeField] private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        detection = ai.GetComponent<HumanFOV>();
        fear = fearbar.GetComponent<FearBar>();
        transform.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.parent.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y + 1, transform.parent.parent.position.z);
        if (detection.detectPlayer && !fear.hidden)
        {
            if(!isdetected)
            {
                alert.SetActive(true);
                isdetected = true;
                AudioManager.instance.PlayOneShot("Alert");
            }
        }
        else
        {
            alert.SetActive(false);
            isdetected = false;
        }
    }
}
