using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class GameManager : MBSingleton<GameManager>
    {
        private static GameManager _instance;
        //private GameplayManager _GameplayManager = GameplayManager.Instance;
        public GameState State;

        public static event Action<GameState> OnGameStateChanged;

        private void Start()
        {
            State = GameState.SayHiToMum;
            //SceneManager.LoadScene("Menu");
            UpdateGameState(GameState.SayHiToMum);
            GamePlayManager.Instance.InitGame(GameLevel.Easy);
        }


        public void UpdateGameState(GameState newState)
        {
            State = newState;
            Debug.Log("GameManager: new state : " + State);
            
            switch (newState)
            {
                
                case GameState.SayHiToMum:
                    SceneManager.LoadScene("Menu");
                    
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
                    SceneManager.LoadScene("WinLoseMenu");
                    break;
                case GameState.Loser:
                    SceneManager.LoadScene("WinLoseMenu");
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
