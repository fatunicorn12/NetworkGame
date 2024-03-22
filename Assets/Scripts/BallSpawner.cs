using UnityEngine;
using Mirror;

public class BallSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float spawnInterval = 10f; 
    private float nextSpawnTime;

    private void Update()
    {
        if (!isServer)
        {
            return;
        }

        if (Time.time >= nextSpawnTime)
        {
            SpawnBall();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnBall()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition(); 

        GameObject ballInstance = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(ballInstance);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0f);
    }
}


