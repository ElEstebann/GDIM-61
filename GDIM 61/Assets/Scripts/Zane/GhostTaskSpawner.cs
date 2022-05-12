using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Zane
public class GhostTaskSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] ghostTasks;

    [SerializeField] private float minimumSpawnInterval;
    [SerializeField] private float maximumSpawnInterval;

    [SerializeField] LayerMask taskLayer;

    private float spawnInterval;
    private int previousTask;
    private int spawnsIndex;
    public int tasksIndex;

    private void Start()
    {
        // tasks start to be activated
        spawnInterval = Random.Range(minimumSpawnInterval, maximumSpawnInterval);
        InvokeRepeating("SpawnTask", 0f, spawnInterval);
    }

    private void SpawnTask()
    {
        // makes sure the same task isn't activated twice in a row
        do
        {
            // gets random index from tasks array
            tasksIndex = Random.Range(0, ghostTasks.Length);

            // each task is activated at it's corresponding spawnpoint
            spawnsIndex = tasksIndex;
        }
        while (previousTask == tasksIndex && ghostTasks.Length > 1);

        // sets previous task activated
        previousTask = tasksIndex;

        // raycasts a circle around the spawnpoint
        Collider2D[] spawnCheck = Physics2D.OverlapCircleAll(spawnPoints[spawnsIndex].position, 1.0f, taskLayer);

        // checks if the task has already spawned in
        if (spawnCheck.Length > 0)
        {
            // task already spawned in
        }
        else
        {
            // random task is spawned in
            Instantiate(ghostTasks[tasksIndex], spawnPoints[spawnsIndex].position, spawnPoints[spawnsIndex].rotation);

            ///ghostTasks[tasksIndex].SetActive(true);
        }
    }
}
