using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;

    public event Action<bool> OnPlayerHitWall;

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
    
    
}
