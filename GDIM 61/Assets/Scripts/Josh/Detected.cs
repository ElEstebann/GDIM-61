using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour
{
    [SerializeField] GameObject ai;
    private HumanFOV detection;
    private SpriteRenderer alert;
   // [SerializeField] private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        detection = ai.GetComponent<HumanFOV>();
        alert = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.parent.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y + 1, transform.parent.parent.position.z);
        if (detection.detectPlayer)
        {
            alert.sortingLayerName = "Character";
        }
        else
        {
            alert.sortingLayerName = "Background";
        }
    }
}
