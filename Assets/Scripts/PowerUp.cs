using UnityEngine;
using Mirror;

public enum PowerUpType
{
    SpeedBoost,
    SlowDown
}

public class PowerUp : NetworkBehaviour
{
    public PowerUpType powerUpType;
    public AudioClip pickUpSound;
    
    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        AudioSource.PlayClipAtPoint(pickUpSound, transform.position, 1f); 
        RpcApplyPowerUp(other.gameObject);
        NetworkServer.Destroy(gameObject);
    }
}

    [ClientRpc]
    private void RpcPlayPickUpSound(Vector3 position)
    {
        if (pickUpSound != null)
        {
            AudioSource.PlayClipAtPoint(pickUpSound, position, 1f);
        }
        else
        {
            Debug.LogWarning("PickUpSound AudioClip is null.");
        }
    }

    [ClientRpc]
    private void RpcApplyPowerUp(GameObject player)
    {
        
        if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                switch (powerUpType)
                {
                    case PowerUpType.SpeedBoost:
                        playerMovement.SetSpeedMultiplier(2f);
                        player.GetComponent<SpriteRenderer>().color = Color.red;
                        break;
                    case PowerUpType.SlowDown:
                        playerMovement.SetSpeedMultiplier(0.5f);
                        player.GetComponent<SpriteRenderer>().color = Color.blue;
                        break;
                }
            }
        }
    }
}
