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
    private int tasks;
    private int spawns;

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
            tasks = Random.Range(0, ghostTasks.Length);

            // each task is activated at it's corresponding spawnpoint
            spawns = tasks;
        }
        while (previousTask == tasks && ghostTasks.Length > 1);

        // sets previous task activated
        previousTask = tasks;

        // raycasts a circle around the spawnpoint
        Collider2D[] spawnCheck = Physics2D.OverlapCircleAll(spawnPoints[spawns].position, 1.0f, taskLayer);

        // checks if the task has already spawned in
        if (spawnCheck.Length > 0)
        {
            // task already spawned in
        }
        else
        {
            // random task is spawned in
            Instantiate(ghostTasks[tasks], spawnPoints[spawns].position, spawnPoints[spawns].rotation);
        }
    }
}
