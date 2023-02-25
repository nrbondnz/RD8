using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Managers;
using Object = UnityEngine.Object;
using String = System.String;

namespace Managers
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

        
        

        private void Start()
        {
            
            SceneManager.LoadSceneAsync("WinLoseMenu");
            GameStatusManager.GetInstance().ResetGameState();

        }


        public void UpdateGameState(GamePhases newPhase, int newLevel)
        {
            GameStatusManager.GetInstance().GamePhases = newPhase;
            GameStatusManager.GetInstance().SceneNum = newLevel;
            Debug.Log(
                "GameManager: new state : " + GameStatusManager.GetInstance());
            
            
            switch (GameStatusManager.GetInstance().GamePhases)
            {
                
                case GamePhases.SayHiToMum:
                    //SceneManager.LoadSceneAsync("Menu");
                    break;
                case GamePhases.GamePlaying:
                    //State.SceneNum++;
                    SceneManager.LoadSceneAsync("Level " + GameStatusManager.GetInstance().SceneNum);
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

            
        }
    }

 
}
