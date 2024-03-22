using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SimpleCoinSpawner : NetworkBehaviour
{
    public GameObject coinPrefab;

    public override void OnStartServer()
    {
        SpawnCoin();
    }

    [Server]
    void SpawnCoin()
    {
        var spawnPosition = new Vector3(0, 0, 0); 
        var coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(coin);
        Debug.Log("Coin Spawned at " + spawnPosition);
    }

    void Start()
    {
        
        if (isServer) 
        {
            var spawnPosition = new Vector3(0, 0, 0); 
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity); 
            Debug.Log("Coin Instantiated at " + spawnPosition);
        }
    }
}

