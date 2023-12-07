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
    /// <summary>
    /// 
    /// Manager the overall game with a single instance GameManager
    /// Initialises the game on the start menu
    /// Manages the scene updates
    /// </summary>
    /// \image html SequenceDiagrams\GameInit.svg "Game Initialisation" width=100%
    public class GameManager : MonoBehaviour
    {

        /// <summary>
        /// Sets up the game manager as a new singleton instance
        /// </summary>
        private void Awake()
        {
            if (Singleton != null)
            {
                Debug.Log("GameManager Trying second Awake");
                Destroy(gameObject);
                return;
            }
            Debug.Log("GameManager Awake");
            Singleton = this as GameManager;

            DontDestroyOnLoad(gameObject);
        }
        
        public static GameManager Singleton
        {
            get;
            set;
        }
 
        /// <summary>
        /// Sets the first scene of the game, WinLoseMEnu and resets the game state
        /// </summary>
        private void Start()
        {
            GameStateManager.Singleton.ResetGameState();
            SceneManager.LoadSceneAsync("WinLoseMenu");
        }
        
        /// <summary>
        /// Updates the scene based on the new gamephase and requested level
        /// Update </summary>
        /// <param name="newPhase"></param>
        /// <param name="newLevel"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void UpdateGameScene(GamePhase newPhase, int newLevel)
        {
            GameStateManager.Singleton.GamePhase = newPhase; 
            GameStateManager.Singleton.SceneNum = newLevel;
            Debug.Log(
                "GameManager: new state : " + GameStateManager.Singleton);
            
            switch (GameStateManager.Singleton.GamePhase)
            {
                case GamePhase.ReadyToGo:
                    //SceneManager.LoadSceneAsync("Menu");
                    break;
                case GamePhase.GamePlaying:
                    //State.SceneNum++;
                    SceneManager.LoadSceneAsync("Level " + GameStateManager.Singleton.SceneNum);
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

        /// <summary>
        /// Checks if the single instance has been created, if it has there will be a single instance of GameManager
        /// Already.  This method ensures whatever scene you are on in edit if required the managers will be started and then the game intro
        /// </summary>
        /// <returns>bool</returns>
        public static bool hasBootSceneRun()
        {
            return Singleton != null;
        }
    }
}