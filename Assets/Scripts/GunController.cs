using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class GunController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;

    
    public void Shoot(Vector2 direction)
    {
        Debug.Log("Shoot called."); 

        if (IsServer)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * bulletSpeed;

            var bulletNetObj = bullet.GetComponent<NetworkObject>();
            bulletNetObj.Spawn();
        }
    }
}




