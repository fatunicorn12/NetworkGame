using UnityEngine;
using Mirror;

public class BallInteraction : NetworkBehaviour
{
    public AudioClip bounceSound;
    public float forceAmount = 5f; 

    private AudioSource audioSource;

    public override void OnStartServer()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = bounceSound;
    }

    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 bounceDirection = (transform.position - collision.transform.position).normalized;

        
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(bounceDirection * forceAmount, ForceMode2D.Impulse);

            RpcPlayBounceSound();
        }
    }

    [ClientRpc]
    void RpcPlayBounceSound()
    {
        if (audioSource && bounceSound)
        {
            audioSource.PlayOneShot(bounceSound);
        }
    }
}
