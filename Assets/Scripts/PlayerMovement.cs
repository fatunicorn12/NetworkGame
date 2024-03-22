using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float baseSpeed = 5f;
    [SyncVar] private float speedMultiplier = 1f;

    void Update()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
            transform.position += movement * baseSpeed * speedMultiplier * Time.deltaTime;
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}

