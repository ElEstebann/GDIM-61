using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour
{
    [SerializeField] GameObject ai;
    [SerializeField] private GameObject alert;
    private HumanFOV detection;
   // [SerializeField] private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        detection = ai.GetComponent<HumanFOV>();
        transform.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.parent.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y + 1, transform.parent.parent.position.z);
        if (detection.detectPlayer)
        {
            alert.SetActive(true);
        }
        else
        {
            alert.SetActive(false);
        }
    }
}
