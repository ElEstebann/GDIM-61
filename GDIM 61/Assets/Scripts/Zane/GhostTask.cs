using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Written by Zane
public class GhostTask : MonoBehaviour
{
    [SerializeField] private KeyCode holdKey;

    [SerializeField] private string taskFailedMessage;
    [SerializeField] private float radius;
    [SerializeField] private float holdTime;
    [SerializeField] private float taskDuration;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject alertIcon;
    [SerializeField] private GameObject arrowTarget;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Slider taskProgressBar;
    [SerializeField] private Animator taskAnimator;
    [SerializeField] private Target target;

    private MovementScript playerMoveScript;

    private float keyHeldStartTime = 0f;
    private float keyHeldTimer;
    private float taskTimer;
    private float taskProgressValue;
    private float taskProgressAdd;
    private bool pauseTaskTimer = false;
    private bool taskCompleted = false;
    private bool keyHeld = false;
    private bool keyHolding;
    private bool fixing;
    private bool playing;

    public bool inRange { get; private set; }
    public static bool taskDone;

    public static string loseMessageText;

    void Start()
    {
        playing = true;
        taskProgressValue = 0;
        taskTimer = taskDuration;
        taskAnimator.SetBool("Fixed", true);

        // finds player object
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        Player = GameObject.FindGameObjectWithTag("Player");

        // assigns components to variables
        taskAnimator = GetComponentInParent<Animator>();
        playerMoveScript = Player.GetComponent<MovementScript>();
        target = arrowTarget.GetComponent(typeof(Target)) as Target;

        StartCoroutine(RangeCheck());

    }

    private void Update()
    {
        // checks if task is not fixed
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
            if (Input.GetKey(holdKey) && keyHeld == false && fixing == true)
            {
                progressBar.SetActive(true);

                // task progress bar
                taskProgressAdd = 100 / holdTime;
                taskProgressValue += taskProgressAdd * Time.deltaTime;
                taskProgressBar.value = taskProgressValue;

                // task hold timer
                keyHeldTimer += Time.deltaTime;
                pauseTaskTimer = true;
                fixing = true;

                // when the key is held down for the required time, the timer stops and the function is called
                if (keyHeldTimer >= (keyHeldStartTime + holdTime))
                {
                    keyHeld = true;
                    taskDone = true;
                    fixing = false;
                    playerMoveScript.SetFixingBool(false);

                    KeyHeld();
                }
            }

            // checks if the fix key has been released to allow the task to be fixed again
            if (Input.GetKeyUp(holdKey))
            {
                pauseTaskTimer = false;
                keyHeld = false;
                fixing = false;
                playerMoveScript.SetFixingBool(false);
            }

            // checks if the fix key has been held to allow the fixing animation to be played again
            if (Input.GetKeyDown(holdKey))
            {
                fixing = true;
                playerMoveScript.SetFixTrigger();
                playerMoveScript.SetFixingBool(true);
                playMeepSound();
            }

            // checks if the task is being fixed to show the task progress bar
            if (fixing == true)
            {
                progressBar.SetActive(true);
            }
            else
            {
                progressBar.SetActive(false);
            }
        }
        else
        {
            progressBar.SetActive(false);
        }
    }

    private void TaskTimer()
    {
        // checks if timer has reached zero and if the fix key is held down
        if (taskTimer > 0 && pauseTaskTimer == false)
        {
            // timer counts down
            taskTimer -= Time.deltaTime;
        }
        else if (pauseTaskTimer == false)
        {
            // checks if the task was completed
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
        AudioManager.instance.Stop("MainTheme");

        loseMessageText = taskFailedMessage;

        GameManager.LoseScreen();
    }

    private void KeyHeld()
    {
        Debug.Log("TASK COMPLETE!! Key held down for " + holdTime + " seconds.");

        // resets task
        taskTimer = taskDuration;
        keyHeldTimer = 0f;
        taskProgressValue = 0;
        keyHeld = false;

        // resets task animation to default
        alertIcon.SetActive(false);
        arrowTarget.SetActive(false);
        taskAnimator.SetBool("Fixed", true);
        taskAnimator.ResetTrigger("Danger");
        taskAnimator.ResetTrigger("Extreme");

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

        // checks if the player is within range of the task for the key to be pressed
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

    //private void OnDrawGizmos()
    //{
    //    if (playing == true)
    //    {
    //       if (taskAnimator.GetBool("Fixed") == false)
    //        {
    //            // creates a visible circle in the gizmos that represents the key press range of the task
    //            UnityEditor.Handles.color = Color.white;
    //            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

    //            // if the player is in range of the task, then the circle turns green
    //            if (inRange)
    //            {
    //                UnityEditor.Handles.color = Color.green;
    //                UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    //            }
    //        }
    //    }
    //}

    private void updateArrowColor()
    {
        if(target && target.enabled && arrowTarget.activeSelf && arrowTarget && target != null)
        {
            if(arrowTarget.activeSelf)
            {
                //Debug.Log("Arrow Active");
            }
            else{
                //Debug.Log("Arrow INACTIVE!");
            }
            target = arrowTarget.GetComponent(typeof(Target)) as Target;
            float ratio = (float)(taskTimer/taskDuration);
            //Debug.Log(ratio);
            target.updatePointerColor(ratio);
        }
        
    }

    private void playMeepSound()
    {
        
        string h = "Working" + Random.Range(1,6).ToString();
        //Debug.Log("played meep" + h);
        AudioManager.instance.PlayOneShot(h);
    }

}
