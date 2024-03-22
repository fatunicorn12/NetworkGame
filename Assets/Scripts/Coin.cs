using UnityEngine;
using Mirror;

public class Coin : NetworkBehaviour
{
    [SerializeField] private AudioClip pickUpSound;

    

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            PlayerScore playerScore = other.gameObject.GetComponent<PlayerScore>();
            if (playerScore != null)
            {
                playerScore.IncrementScore(1);
            }

            
            PlayPickUpSound();

            NetworkServer.Destroy(gameObject);
        }
    }

    
    [ClientRpc]
    void PlayPickUpSound()
    {
        if (pickUpSound != null && isClient)
        {
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
        }
    }
}


