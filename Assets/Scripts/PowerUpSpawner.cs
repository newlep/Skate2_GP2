using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Array of power-up prefabs
    public Transform[] spawnPoints; // Array of spawn positions
    public float spawnInterval = 10f; // Time between spawns

    void Start()
    {
        InvokeRepeating(nameof(SpawnPowerUp), 0f, spawnInterval);
    }

    private void SpawnPowerUp()
    {
        if (spawnPoints.Length == 0 || powerUpPrefabs.Length == 0)
            return;

        // Choose a random spawn point and power-up
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject powerUp = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

        // Spawn the power-up
        Instantiate(powerUp, spawnPoint.position, Quaternion.identity);
    }
}
