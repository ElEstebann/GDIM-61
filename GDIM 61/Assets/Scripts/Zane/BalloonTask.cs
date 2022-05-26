using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BalloonTask: MonoBehaviour
{
    [SerializeField] private GameObject powerLines;
    [SerializeField] private KeyCode holdKey;

    [SerializeField] private float radius;
    [SerializeField] private float holdTime;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Slider taskProgressBar;
    [SerializeField] private Animator balloonAnimator;

    private MovementScript playerMoveScript;

    private float keyHeldStartTime = 0f;
    private float keyHeldTimer;
    private float taskProgressValue;
    private float taskProgressAdd;

    private bool keyHeld = false;
    private bool popping;
    private bool collided;
    private bool playing;
    public bool inRange { get; private set; }


    private void Start()
    {
        playing = true;
        taskProgressValue = 0;
        balloonAnimator.SetBool("Floating", false);

        // finds player object
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        Player = GameObject.FindGameObjectWithTag("Player");

        // assigns components to variables
        balloonAnimator = GetComponentInParent<Animator>();
        playerMoveScript = Player.GetComponent<MovementScript>();

        StartCoroutine(RangeCheck());
    }

    private void Update()
    {
        // checks if task is not fixed
        if (balloonAnimator.GetBool("Floating") == true)
        {
            TaskKeyPress();
        }
    }

    private void TaskKeyPress()
    {
        // checks if the player is in range of the task
        if (inRange == true)
        {
            // adds time to the timer while the key is held down
            if (Input.GetKey(holdKey) && keyHeld == false && popping == true)
            {
                playerMoveScript.SetFixingBool(true);
                progressBar.SetActive(true);

                // task progress bar
                taskProgressAdd = 100 / holdTime;
                taskProgressValue += taskProgressAdd * Time.deltaTime;
                taskProgressBar.value = taskProgressValue;

                // task hold timer
                keyHeldTimer += Time.deltaTime;
                popping = true;

                // when the key is held down for the required time, the timer stops and the function is called
                if (keyHeldTimer >= (keyHeldStartTime + holdTime))
                {
                    keyHeld = true;
                    popping = false;
                    playerMoveScript.SetFixingBool(false);

                    KeyHeld();
                }
            }

            // checks if the fix key has been released to allow the task to be fixed again
            if (Input.GetKeyUp(holdKey))
            {
                keyHeld = false;
                popping = false;
                playerMoveScript.SetFixingBool(false);
            }

            // checks if the fix key has been held to allow the fixing animation to be played again
            if (Input.GetKeyDown(holdKey))
            {
                popping = true;
                playerMoveScript.SetFixTrigger();
                playerMoveScript.SetFixingBool(true);
            }

            // checks if the task is being fixed to show the task progress bar
            if (popping == true)
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
            // checks if the fix key is being held while the player is out of range of the task
            if (Input.GetKey(holdKey))
            {
                playerMoveScript.SetFixingBool(false);
            }

            progressBar.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // checks if balloons have collided with the power lines
        if (collision.gameObject == powerLines && collided == false)
        {
            TaskFailed();
            collided = true;
        }
    }

    private void TaskFailed()
    {
        Debug.Log("YOU LOSE!! Failed to pop the balloon before it hit the power line.");
        //SceneManager.LoadScene("Lose");
    }

    private void KeyHeld()
    {
        Debug.Log("TASK COMPLETE!! Key held down for " + holdTime + " seconds.");
        Destroy(this.gameObject);
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

    private void OnDrawGizmos()
    {
        if (playing == true)
        {
            if (balloonAnimator.GetBool("Floating") == true)
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
    }
}
