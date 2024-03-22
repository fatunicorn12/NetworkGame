using UnityEngine;
using Mirror;

public class CollectableCoin : NetworkBehaviour
{
    [SerializeField] private AudioClip pickUpSound;

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RpcPlayPickUpSound(transform.position);
            
            IncrementPlayerScore(other.gameObject);

            NetworkServer.Destroy(gameObject);
        }
    }

    [Server]
    private void IncrementPlayerScore(GameObject player)
    {
        PlayerScore playerScore = player.GetComponent<PlayerScore>();
        if (playerScore != null)
        {
 
            playerScore.IncrementScore(1);
        }
    }

    [ClientRpc]
    void RpcPlayPickUpSound(Vector3 position)
    {
        if (pickUpSound != null)
        {
            AudioSource.PlayClipAtPoint(pickUpSound, position, 1f); 
        }
    }
}



