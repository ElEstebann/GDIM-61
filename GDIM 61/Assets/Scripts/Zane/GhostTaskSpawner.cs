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

    private float spawnInterval;
    private int spawns;
    private int tasks;

    private void Start()
    {
        spawnInterval = Random.Range(minimumSpawnInterval, maximumSpawnInterval);
        InvokeRepeating("SpawnTasks", 5.0f, spawnInterval);
    }

    private void SpawnTasks()
    {
        spawns = Random.Range(0, spawnPoints.Length);
        tasks = Random.Range(0, ghostTasks.Length);

        Instantiate(ghostTasks[tasks], spawnPoints[spawns].position, spawnPoints[spawns].rotation);
    }
}
