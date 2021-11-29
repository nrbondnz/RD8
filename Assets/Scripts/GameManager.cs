using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    private void Awake()
    {
        GameManager.Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.SayHiToMum);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.SayHiToMum:
                break;
            case GameState.FirstScene:
                break;
            case GameState.SecondScene:
                break;
            case GameState.ThirdScene:
                break;
            case GameState.Winner:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }
    
}

public enum GameState
{
    SayHiToMum,
    FirstScene,
    SecondScene,
    ThirdScene,
    Winner
}
