using UnityEngine;
using Mirror;
using TMPro; 

public class ScoreManager : NetworkBehaviour
{
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    private PlayerScore player1Score;
    private PlayerScore player2Score;

    public void SetPlayerScoreReference(GameObject player, int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Score = player.GetComponent<PlayerScore>();
        }
        else if (playerNumber == 2)
        {
            player2Score = player.GetComponent<PlayerScore>();
        }
    }

    void Update()
    {
        if (!isServer) return;

        if (player1Score != null)
        {
            RpcUpdateScoreUI(1, player1Score.GetScore());
        }
        if (player2Score != null)
        {
            RpcUpdateScoreUI(2, player2Score.GetScore());
        }
    }

    [ClientRpc]
    void RpcUpdateScoreUI(int playerNumber, int score)
    {
        if (playerNumber == 1)
        {
            player1ScoreText.text = $"Player 1 Score: {score}";
        }
        else if (playerNumber == 2)
        {
            player2ScoreText.text = $"Player 2 Score: {score}";
        }
    }
}

