using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Zane
public class GhostTask : GhostTaskSpawner
{
    [SerializeField] private KeyCode holdKey;

    [SerializeField] private float radius;
    [SerializeField] private float holdTime;
    [SerializeField] private float taskDuration;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private GameObject Player;
    [SerializeField] private Animator taskAnimator; // added a pointer to the task's animator - Joyce

    private MovementScript playerMoveScript;

    private float keyHeldStartTime = 0f;
    private float keyHeldTimer;
    private float taskTimer;

    private bool pauseTaskTimer = false;
    private bool taskCompleted = false;
    private bool keyHeld = false;
    public static bool taskDone;
    public bool inRange { get; private set; }


    void Start()
    {
        StartCoroutine(RangeCheck());

        taskTimer = taskDuration;

        // added in temp animation stuff - Joyce
        taskAnimator = GetComponentInParent<Animator>();
        taskAnimator.SetTrigger("Danger");
        playerMoveScript = Player.GetComponent<MovementScript>();
    }

    private void Update()
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
                    ButtonHeld();
                    playerMoveScript.SetFixingBool(false);
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
                //timer = startTime;
                playerMoveScript.SetFixTrigger();
                playerMoveScript.SetFixingBool(true);
            }
        }

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
    }

    private void ButtonHeld()
    {
        Debug.Log("TASK COMPLETE!! Key held down for " + holdTime + " seconds.");

        Destroy(this.transform.parent.gameObject);

        // added in changing animation state to fixed and disabling the script component once done - Joyce
        taskAnimator.SetTrigger("Fixed");
        ///this.enabled = false;
    }

    private void TaskFailed()
    {
        Debug.Log("YOU LOSE!! Failed to complete the task in " + taskDuration + " seconds.");
        GameManager.LoseScreen();
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
