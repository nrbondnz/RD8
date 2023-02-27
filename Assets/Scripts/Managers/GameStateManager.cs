using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Managers
{
    public enum GamePhase
    {
        SayHiToMum,
        GamePlaying,
        Winner,
        Loser
    }
    
    public class GameStateManager : MonoBehaviour
    {
    
        private GamePhase _gamePhase = GamePhase.SayHiToMum;
        private int _sceneNum = 0;
        private int _lastLevel = 0;
        private static GameStateManager _instance;
      
        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("GameStateManager Trying second Awake");
                Destroy(gameObject);
                return;
            }

            Debug.Log("GameStateManager Awake");
            _instance = this;
            DontDestroyOnLoad(gameObject);
            SetupLastLevel();
        }

        public static GameStateManager GetInstance()
        {
            return _instance;
        }

        public void ResetGameState()
        {
            this._sceneNum = 0;
            this.GamePhase = GamePhase.SayHiToMum;
        }

        private void SetupLastLevel()
        {
            string [] files = System.IO.Directory.GetFiles("Assets/Scenes/Game Levels/");
            foreach (var aFile in files)
            {
                    Debug.Log(aFile);
                Debug.Log(aFile.EndsWith(".unity"));
                
                if (aFile.EndsWith(".unity"))
                {
                    _lastLevel++;
                }
            }

            //lastLevel = files.Length;
        }
        
        public GameStateManager()
        {
            _gamePhase = GamePhase.SayHiToMum;
            _sceneNum = 0;
        }

        public int LastLevel
        {
            get => _lastLevel;
            set => _lastLevel = value;
        }

        public GamePhase GamePhase
        {
            get => _gamePhase;
            set => _gamePhase = value;
        }

        public int SceneNum
        {
            get => _sceneNum;
            set => _sceneNum = value;
        }
    }
}