using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = target.transform.position - player.transform.position;
    }
}
