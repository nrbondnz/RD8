using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using String = System.String;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        
        //private GameplayManager _GameplayManager = GameplayManager.Instance;
        
       
        

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
            GameState.GetInstance().ResetGameState();

        }


        public void UpdateGameState(GamePhases newPhase, int newLevel)
        {
            GameState.GetInstance().GamePhases = newPhase;
            GameState.GetInstance().SceneNum = newLevel;
            Debug.Log("GameManager: new state : " + GameState.GetInstance());
            
            switch (GameState.GetInstance().GamePhases)
            {
                
                case GamePhases.SayHiToMum:
                    //SceneManager.LoadSceneAsync("Menu");
                    break;
                case GamePhases.GamePlaying:
                    //State.SceneNum++;
                    SceneManager.LoadSceneAsync("Level " + GameState.GetInstance().SceneNum);
                    break;
                case GamePhases.Winner:
                    SceneManager.LoadSceneAsync("WinLoseMenu");
                    break;
                case GamePhases.Loser:
                    SceneManager.LoadSceneAsync("WinLoseMenu");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(State), newPhase, null);
            }

            OnGameStateChanged?.Invoke(GameState.GetInstance());
        }

    }

 
}
