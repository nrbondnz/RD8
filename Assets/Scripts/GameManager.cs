using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class GameManager : Singleton<GameManager>
    {
        private static GameManager _instance;
        private GameplayManager _GameplayManager = new GameplayManager();
        public GameState State;

        public static event Action<GameState> OnGameStateChanged;

        private void Start()
        {
            State = GameState.SayHiToMum;
            //SceneManager.LoadScene("Menu");
            UpdateGameState(GameState.SayHiToMum);
        }


        public void UpdateGameState(GameState newState)
        {
            State = newState;
            Debug.Log("GameManager: new state : " + State);
            switch (newState)
            {
                case GameState.Loser:
                    break;
                case GameState.SayHiToMum:
                    SceneManager.LoadScene("Menu");
                    _GameplayManager.InitGame(GameLevel.Easy);
                    break;
                case GameState.FirstScene:
                    SceneManager.LoadScene(0);
                    break;
                case GameState.SecondScene:
                    SceneManager.LoadScene(1);
                    break;
                case GameState.ThirdScene:
                    SceneManager.LoadScene(2);
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
        Winner,
        Loser
    }
}
