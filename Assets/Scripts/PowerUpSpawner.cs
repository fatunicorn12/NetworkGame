using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PowerUpSpawner : NetworkBehaviour
{
    public GameObject redPowerUpPrefab;
    public GameObject bluePowerUpPrefab;
    public float spawnInterval = 8f;

    private float nextSpawnTime;

    
    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    
    void Update()
{
    if (!isServer)
    {
        return; 
    }

    if (Time.time >= nextSpawnTime)
    {
 
        SpawnRandomPowerUp();
        nextSpawnTime = Time.time + spawnInterval;
    }
}

    void SpawnRandomPowerUp()
{
    if (redPowerUpPrefab == null)
    {
        Debug.LogError("Red PowerUp Prefab is not assigned in PowerUpSpawner.");
    }
    if (bluePowerUpPrefab == null)
    {
        Debug.LogError("Blue PowerUp Prefab is not assigned in PowerUpSpawner.");
    }

    GameObject powerUpToSpawn = Random.value < 0.5 ? redPowerUpPrefab : bluePowerUpPrefab;
    if (powerUpToSpawn == null)
    {
        Debug.LogError("Selected PowerUp Prefab is null. Cannot spawn.");
        return; 
    }

    Vector3 spawnPosition = GetRandomSpawnPosition();
    GameObject spawnedPowerUp = Instantiate(powerUpToSpawn, spawnPosition, Quaternion.identity);
    NetworkServer.Spawn(spawnedPowerUp);
}



    Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-8f, 8f);
        float y = Random.Range(-5f, 5f);
        return new Vector3(x, y, 0);
    }
}

