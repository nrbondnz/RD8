using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        
        //private GameplayManager _GameplayManager = GameplayManager.Instance;
        public GameState State;
       
        

        private static GameManager _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("GameManager Trying second Awake");
                Destroy(gameObject);
                return;
            }
            Debug.Log("GamePlayManager Awake");
            _instance = this as GameManager;
            DontDestroyOnLoad(gameObject);
        }

        public static GameManager GetInstance()
        {
            return _instance;
        }

        
        public static event Action<GameState> OnGameStateChanged;

        private void Start()
        {
            //State = GameState.SayHiToMum;
            //SceneManager.LoadScene("Menu");
            //UpdateGameState(GameState.Winner);
            //GamePlayManager.Instance.InitGame(GameLevel.Easy);
            SceneManager.LoadSceneAsync("WinLoseMenu");
        }


        public void UpdateGameState(GameState newState)
        {
            State = newState;
            Debug.Log("GameManager: new state : " + State);
            
            switch (newState)
            {
                
                case GameState.SayHiToMum:
                    //SceneManager.LoadSceneAsync("Menu");
                    break;
                case GameState.FirstScene:
                    SceneManager.LoadSceneAsync("Level 1");
                    break;
                case GameState.SecondScene:
                    SceneManager.LoadSceneAsync("Level 4");
                    break;
                case GameState.ThirdScene:
                    SceneManager.LoadSceneAsync("Level 2");
                    break;
                case GameState.ForthScene:
                    SceneManager.LoadSceneAsync("Level 3");
                    break;
                case GameState.Winner:
                    SceneManager.LoadSceneAsync("WinLoseMenu");
                    break;
                case GameState.Loser:
                    SceneManager.LoadSceneAsync("WinLoseMenu");
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
        ForthScene,
        Winner,
        Loser
    }
}
