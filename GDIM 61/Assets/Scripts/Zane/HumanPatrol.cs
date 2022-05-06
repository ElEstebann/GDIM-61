using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Zane
public class HumanPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform humanAI;

    [SerializeField] private float humanSpeed;
    [SerializeField] private float idleDuration;

    [SerializeField] private Animator anim;

    private bool movingLeft;
    private float idleTimer;
    private Vector3 initialScale;

    private void Awake()
    {
        initialScale = humanAI.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            // detects if the AI has reached the left edge
            if(humanAI.position.x - 0.75 > leftEdge.position.x)
            {
                // the AI moves to the left
                MoveInDirection(-1);
            }
            else
            {
                // the AI changes direction
                DirectionChange();
            }
        }
        else
        {
            // detects if the AI has reached the right edge
            if (humanAI.position.x + 0.75 < rightEdge.position.x)
            {
                // the AI moves to the right
                MoveInDirection(1);
            }
            else
            {
                // the AI changes direction
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        // deactivates walking animation
        anim.SetBool("Walk", false);

        // changes direction of the AI as soon as it reaches the edge
        if (movingLeft)
        {
            //humanAI.localScale = new Vector3(Mathf.Abs(initialScale.x) * 1, initialScale.y, initialScale.z);
        }
        else
        {
            //humanAI.localScale = new Vector3(Mathf.Abs(initialScale.x) * -1, initialScale.y, initialScale.z);
        }

        // starts idle timer
        idleTimer += Time.deltaTime;

        // checks if AI has idled long enough
        if (idleTimer > idleDuration)
        {
            // allows for AI to change direction
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int direction)
    {
        // activates walking animation
        anim.SetBool("Walk", true);

        // resets idle timer
        idleTimer = 0;

        // makes the AI face a direction
        humanAI.localScale = new Vector3(Mathf.Abs(initialScale.x) * direction, initialScale.y, initialScale.z);

        // makes the AI move in the direction it's facing
        humanAI.position = new Vector3(humanAI.position.x + Time.deltaTime * direction * humanSpeed, humanAI.position.y, humanAI.position.z);
    }
}
