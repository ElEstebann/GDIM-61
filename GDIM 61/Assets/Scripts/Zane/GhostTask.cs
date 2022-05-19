using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Zane
public class GhostTask : MonoBehaviour
{
    [SerializeField] private KeyCode holdKey;

    [SerializeField] private float radius;
    [SerializeField] private float holdTime;
    [SerializeField] private float taskDuration;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject alertIcon;
    [SerializeField] private GameObject arrowTarget;
    [SerializeField] private Animator taskAnimator;

    private MovementScript playerMoveScript;

    private float keyHeldStartTime = 0f;
    private float keyHeldTimer;
    private float taskTimer;

    private bool pauseTaskTimer = false;
    private bool taskCompleted = false;
    private bool keyHeld = false;
    private bool playing;
    public static bool taskDone;
    public bool inRange { get; private set; }
    [SerializeField] private Target target;


    void Start()
    {
        playing = true;
        StartCoroutine(RangeCheck());
        taskTimer = taskDuration;
        taskAnimator.SetBool("Fixed", true);

        taskAnimator = GetComponentInParent<Animator>();
        playerMoveScript = Player.GetComponent<MovementScript>();
        Player = GameObject.FindGameObjectWithTag("Player");
        target = arrowTarget.GetComponent(typeof(Target)) as Target;

    }

    private void Update()
    {
        if (taskAnimator.GetBool("Fixed") == false)
        {
            TaskKeyPress();
            TaskTimer();
            updateArrowColor();
        }
        else
        {
            pauseTaskTimer = false;
        }
    }

    private void TaskKeyPress()
    {
        // checks if the player is in range of the task
        if (inRange == true)
        {
            // adds time to the timer while the key is held down
            if (Input.GetKey(holdKey) && keyHeld == false)
            {
                keyHeldTimer += Time.deltaTime;
                playerMoveScript.SetFixingBool(true);

                // when the key is held down for the required time, the timer stops and the function is called
                if (keyHeldTimer >= (keyHeldStartTime + holdTime))
                {
                    keyHeld = true;
                    taskDone = true;
                    playerMoveScript.SetFixingBool(false);

                    KeyHeld();
                }

                pauseTaskTimer = true;
            }

            // allows for key to be pressed again
            if (Input.GetKeyUp(holdKey))
            {
                keyHeld = false;
                pauseTaskTimer = false;
                playerMoveScript.SetFixingBool(false);
            }

            // resets the timer when the key is pressed again
            if (Input.GetKeyDown(holdKey))
            {
                ///timer = startTime;
                playerMoveScript.SetFixTrigger();
                playerMoveScript.SetFixingBool(true);
            }
        }
    }

    private void TaskTimer()
    {
        // checks if timer has reached zero and if key is held down
        if (taskTimer > 0 && pauseTaskTimer == false)
        {
            // timer counts down
            taskTimer -= Time.deltaTime;
        }
        else if (pauseTaskTimer == false)
        {
            // checks if task was completed
            if (!taskCompleted)
            {
                TaskFailed();
                taskCompleted = true;
            }
        }

        // activates extreme animation when the task timer is 10 seconds or less
        if (taskTimer <= 10)
        {
            taskAnimator.SetTrigger("Extreme");
        }
    }

    private void TaskFailed()
    {
        Debug.Log("YOU LOSE!! Failed to complete the task in " + taskDuration + " seconds.");
        GameManager.LoseScreen();
    }

    private void KeyHeld()
    {
        Debug.Log("TASK COMPLETE!! Key held down for " + holdTime + " seconds.");

        // resets task
        taskTimer = taskDuration;
        keyHeldTimer = 0f;
        keyHeld = false;

        // resets task animation to default
        alertIcon.SetActive(false);
        arrowTarget.SetActive(false);
        taskAnimator.SetBool("Fixed", true);
        taskAnimator.ResetTrigger("Danger");

        ///Destroy(this.transform.parent.gameObject);
        ///this.enabled = false;
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
        if (playing == true)
        {
           /* if (taskAnimator.GetBool("Fixed") == false)
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
            }*/
        }
    }

    private void updateArrowColor()
    {
        if(target.enabled)
        {
            float ratio = (float)(taskTimer/taskDuration);
            //Debug.Log(ratio);
            target.updatePointerColor(ratio);
        }
        
    }
}
