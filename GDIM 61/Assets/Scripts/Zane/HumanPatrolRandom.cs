using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Zane
public class HumanPatrolRandom : MonoBehaviour
{
    [SerializeField] private float humanSpeed;
    [SerializeField] private float idleDuration;

    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform humanAI;
    [SerializeField] private Animator anim;

    [SerializeField] private Transform[] moveLocations;

    private int randomPosition;
    private float idleTimer;
    private bool facingLeft = false;

    private Vector3 initialScale;

    private void Awake()
    {
        initialScale = humanAI.localScale;
    }

    private void Start()
    {
        idleTimer = idleDuration;
        randomPosition = Random.Range(0, moveLocations.Length);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveLocations[randomPosition].position, humanSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveLocations[randomPosition].position) < 0.2f)
        {
            if (idleTimer <= 0)
            {
                randomPosition = Random.Range(0, moveLocations.Length);
                idleTimer = idleDuration;
            }
            else
            {
                idleTimer -= Time.deltaTime;
            }
        }

        if (facingLeft == true)
        {
            // detects if the AI has reached the left edge
            if (humanAI.position.x > moveLocations[randomPosition].transform.position.x)
            {
                // the AI is moving to the left
                MoveDirection(-1);

            }
            else
            {
                // the AI changes direction after idle duration
                DirectionChange(1);
            }
        }
        else
        {
            // detects if the AI has reached the right edge
            if (humanAI.position.x < moveLocations[randomPosition].transform.position.x)
            {
                // the AI is moving to the right
                MoveDirection(1);

            }
            else
            {
                // the AI changes direction after idle duration
                DirectionChange(-1);
            }
        }
    }

    private void DirectionChange(int direction)
    {
        // deactivates walking animation
        anim.SetBool("Walk", false);

        if (direction == 1)
        {
            facingLeft = false;
        }
        else if (direction == -1)
        {
            facingLeft = true;
        }
    }

    private void MoveDirection(int direction)
    {
        // activates walking animation
        anim.SetBool("Walk", true);

        // makes the AI face a direction
        humanAI.localScale = new Vector3(Mathf.Abs(initialScale.x) * direction, initialScale.y, initialScale.z);
    }
}
