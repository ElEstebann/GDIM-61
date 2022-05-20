using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Zane
public class GhostTaskActivator : MonoBehaviour
{
    ///[SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] ghostTasks;
    [SerializeField] private GameObject[] alertIcons;
    [SerializeField] private GameObject[] arrowTargets;
    [SerializeField] private Animator[] taskAnimators;

    [SerializeField] private float minimumSpawnInterval;
    [SerializeField] private float maximumSpawnInterval;

    [SerializeField] LayerMask taskLayer;

    private float spawnInterval;
    private int previousTask;
    private int tasksIndex;
    ///private int spawnsIndex;

    private void Start()
    {
        // tasks start to be activated
        spawnInterval = Random.Range(minimumSpawnInterval, maximumSpawnInterval);
        InvokeRepeating("SpawnTask", 0f, spawnInterval);
        AudioManager.instance.Play("MainTheme");
    }

    private void SpawnTask()
    {
        // makes sure the same task isn't activated twice in a row
        do
        {
            // gets random index from tasks array
            tasksIndex = Random.Range(0, ghostTasks.Length);

            // each task is activated at it's corresponding spawnpoint
            ///spawnsIndex = tasksIndex;
        }
        while (previousTask == tasksIndex && ghostTasks.Length > 1);

        // sets previous task activated
        previousTask = tasksIndex;

        // raycasts a circle around the spawnpoint
        ///Collider2D[] spawnCheck = Physics2D.OverlapCircleAll(spawnPoints[spawnsIndex].position, 1.0f, taskLayer);

        // checks if the task has already bee activated
        if (taskAnimators[tasksIndex].GetBool("Fixed") == false)
        {
            // task already activated
        }
        else
        {
            // random task is spawned in
            ///Instantiate(ghostTasks[tasksIndex], spawnPoints[spawnsIndex].position, spawnPoints[spawnsIndex].rotation);

            // random task is activated
            taskAnimators[tasksIndex].SetBool("Fixed", false);
            taskAnimators[tasksIndex].SetTrigger("Danger");
            alertIcons[tasksIndex].SetActive(true);
            arrowTargets[tasksIndex].SetActive(true);
        }
    }
}
