using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace Managers
{
    /// <summary>
    /// Sets up the GamePhase enum so the state manager can control and notify the phase to suscribed classes
    /// </summary>
    public enum GamePhase
    {
        ReadyToGo,
        GamePlaying,
        Winner,
        Loser
    }
    
    /// <summary>
    /// Game state manager controls the GamePhase and when running the current scene number and last level
    /// scene number before winning the overall game
    /// </summary>
    public class GameStateManager : MonoBehaviour
    {
    
        private GamePhase _gamePhase = GamePhase.ReadyToGo;
        private int _sceneNum = 1;
        private int _finalLevel = 0;
        //private static GameStateManager _instance;
        public static GameStateManager Singleton
        {
            get;
            set;
        }
        /// <summary>
        /// Initializes a new instance of the GameStateManager and ensures that there is only one instance
        /// </summary>
        private void Awake()
        {
            if (Singleton != null)
            {
                Debug.Log("GameStateManager Trying second Awake");
                Destroy(gameObject);
                return;
            }

            Debug.Log("GameStateManager Awake");
            Singleton = this;
            DontDestroyOnLoad(gameObject);
            SetupLastLevel();
        }

        /// <summary>
        /// gets the (single per run) instance of the GameStateManager
        /// which will be created during Awake
        /// </summary>
        /// <returns>GameStateManager</returns>


        /// <summary>
        /// At initialization the Assets are inspected to determine the available game levels within the
        /// Assets/Scenes/Game Levels repository
        /// </summary>
        private void SetupLastLevel()
        {
            Debug.Log("~~~~~~ Scene List ~~~~~~");
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            Debug.Log("~~~~ Count :" + sceneCount + " ~~~~");
            for (int i = 0; i < sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string[] splitScenePath = scenePath.Split('/');
                string sceneFile = splitScenePath[splitScenePath.Length - 1];
                string sceneName = sceneFile.Split('.')[0];
                Debug.Log("~~ " + sceneName + " ~~");
                if (sceneName.Contains("Level "))
                {
                    _finalLevel++;
                }
            }
            Debug.Log("Final level is : " + _finalLevel);
        }
        /*private void SetupLastLevel()
        {
            string [] files = System.IO.Directory.GetFiles("/Assets/Scenes/Game Levels/");
            foreach (var aFile in files)
            {
                Debug.Log(aFile);
               
                
                if (aFile.EndsWith(".unity"))
                {
                    Debug.Log(aFile.EndsWith(".unity"));
                    _finalLevel++;
                }
            }
            Debug.Log("Final level : " + _finalLevel);
            //lastLevel = files.Length;
        }*/

        /// <summary>
        /// At start of play the game state is reset for the single instance of GameStateManager
        /// </summary>
        public void ResetGameState()
        {
            this._sceneNum = 1;
            this.GamePhase = GamePhase.ReadyToGo;
        }
        
        /// <summary>
        /// Setup GameStateManager when first (and only) instance initialisation
        /// </summary>
        public GameStateManager()
        {
            _gamePhase = GamePhase.ReadyToGo;
            _sceneNum = 1;
        }

        /// <summary>
        /// Get/Set of LastLevel to be used on initialization
        /// </summary>
        public int LastLevel
        {
            get => _finalLevel;
            set => _finalLevel = value;
        }

        /// <summary>
        /// Sets up Game Phase get/set
        /// </summary>
        public GamePhase GamePhase
        {
            get => _gamePhase;
            set => _gamePhase = value;
        }

        /// <summary>
        /// get/set of SceneNum
        /// </summary>
        public int SceneNum
        {
            get => _sceneNum;
            set => _sceneNum = value;
        }
    }
}