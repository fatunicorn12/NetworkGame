using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameTimer : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SyncVar(hook = nameof(OnTimerChanged))] private float remainingTime;
    private bool timerStarted = false;

    private void Start()
    {
        if (isServer) 
        {
            remainingTime = 60f; 
        }
    }

    private void Update()
    {
        if (isServer)
        {
            if (NetworkManager.singleton.numPlayers == 2 && !timerStarted)
            {
                timerStarted = true; 
                RpcStartTimer();
            }

            if (timerStarted && remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                if (remainingTime <= 0)
                {
                    RpcRestartScene();
                }
            }
        }
    }


    void OnTimerChanged(float oldTime, float newTime)
    {
        timerText.text = Mathf.CeilToInt(newTime).ToString();
    }

    [ClientRpc]
    void RpcStartTimer()
    {
        timerStarted = true;
    }

    [ClientRpc]
    void RpcRestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

