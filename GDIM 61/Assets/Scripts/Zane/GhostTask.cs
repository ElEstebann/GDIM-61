using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTask : MonoBehaviour
{
    [SerializeField] private KeyCode holdKey;

    [SerializeField] private float radius;
    [SerializeField] private float holdTime;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask wallLayer;

    [SerializeField] GameObject Player;

    private float startTime = 0f;
    private float timer = 0f;

    private bool keyHeld = false;
    public bool inRange { get; private set; }


    void Start()
    {
        StartCoroutine(RangeCheck());
    }

    private void Update()
    {
        // checks if the player is in range of the task
        if (inRange == true)
        {
            // starts the timer when the key is pressed
            if (Input.GetKeyDown(holdKey))
            {
                timer = startTime;
            }

            // adds time to the timer while the key is held down
            if (Input.GetKey(holdKey) && keyHeld == false)
            {
                timer += Time.deltaTime;

                // when the key is held down for the required time, the timer stops and the function is called
                if (timer >= (startTime + holdTime))
                {
                    keyHeld = true;
                    ButtonHeld();
                }
            }

            // allows for key to be pressed again
            if (Input.GetKeyUp(holdKey))
            {
                keyHeld = false;
            }
        }
    }

    private void ButtonHeld()
    {
        Debug.Log("TASK COMPLETE!! Key held down for " + holdTime + " seconds.");
    }

    private IEnumerator RangeCheck()
    {
        // checks if the player is in range every 0.2 seconds
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            ButtonPressRange();
        }
    }

    private void ButtonPressRange()
    {
        // raycasts a circle around the task
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, playerLayer);

        // checks if the player is within range of the task for the button to be pressed
        if (rangeCheck.Length > 0)
        {
            Transform player = rangeCheck[0].transform;
            Vector2 directionToPlayer = (player.position - transform.position).normalized;

            float distanceToPlayer = Vector2.Distance(transform.position * transform.localScale.x, player.position);

            // checks if the player is in the direct line of sight of the task
            if (!Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, wallLayer))
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }
        }
        else if (inRange)
        {
            inRange = false;
        }
    }

    private void OnDrawGizmos()
    {
        // creates a visible circle in the gizmos that represents the key press range of the task
        UnityEditor.Handles.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        // if the player is in range of the task, then the circle turns green
        if (inRange)
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
        }
    }
}
