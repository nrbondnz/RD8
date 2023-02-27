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
 
        /// <summary>
        /// Sets the first scene of the game, WinLoseMEnu and resets the game state
        /// </summary>
        private void Start()
        {
            SceneManager.LoadSceneAsync("WinLoseMenu");
            GameStateManager.GetInstance().ResetGameState();
        }
        
        /// <summary>
        /// Updates the scene based on the new gamephase
        /// Update </summary>
        /// <param name="newPhase"></param>
        /// <param name="newLevel"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void UpdateGameScene(GamePhase newPhase, int newLevel)
        {
            GameStateManager.GetInstance().GamePhase = newPhase; GameStateManager.GetInstance().SceneNum = newLevel;
            Debug.Log(
                "GameManager: new state : " + GameStateManager.GetInstance());
            
            switch (GameStateManager.GetInstance().GamePhase)
            {
                case GamePhase.SayHiToMum:
                    //SceneManager.LoadSceneAsync("Menu");
                    break;
                case GamePhase.GamePlaying:
                    //State.SceneNum++;
                    SceneManager.LoadSceneAsync("Level " + GameStateManager.GetInstance().SceneNum);
                    break;
                case GamePhase.Winner:
                    SceneManager.LoadSceneAsync("WinLoseMenu");
                    break;
                case GamePhase.Loser:
                    SceneManager.LoadSceneAsync("WinLoseMenu");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(State), newPhase, null);
            }
        }
    }
}