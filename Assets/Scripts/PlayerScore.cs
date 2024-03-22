using UnityEngine;
using Mirror;

public class PlayerScore : NetworkBehaviour
{
    [SyncVar]
    private int score = 0;

    public void IncrementScore(int amount)
    {
        if (isServer) 
        {
            score += amount;
        }
    }

    public int GetScore()
    {
        return score;
    }
}


