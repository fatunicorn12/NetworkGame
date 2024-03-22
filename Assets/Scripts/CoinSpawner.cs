using UnityEngine;
using Mirror;

public class CoinSpawner : NetworkBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 5f;
    private float nextSpawnTime;

    void Update()
    {
        if (!isServer)
        {
            return;
        }

        if (Time.time >= nextSpawnTime)
        {
            SpawnCoin();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnCoin()
{
    Vector3 spawnPosition = GetRandomSpawnPosition(); 
    GameObject coinInstance = Instantiate(coinPrefab, spawnPosition, Quaternion.identity); 
    NetworkServer.Spawn(coinInstance); 
}

    Vector3 GetRandomSpawnPosition()
    {

        return new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);
    }

    public void RepositionCoin(GameObject coin)
    {
        Vector3 newSpawnPosition = GetRandomSpawnPosition();
        coin.transform.position = newSpawnPosition;
    }

}




