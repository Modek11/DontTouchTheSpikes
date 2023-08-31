using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;

    public event Action<bool> OnPlayerHitWall;
    public event Action OnPlayerDie;

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InvokeOnPlayerHitWall(bool isFlyingRight)
    {
        OnPlayerHitWall?.Invoke(isFlyingRight);
    }
    
    public void InvokeOnPlayerDie()
    {
        OnPlayerDie?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    
}
