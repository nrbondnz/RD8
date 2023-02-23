using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game
{
    public enum GamePhases
    {
        SayHiToMum,
        GamePlaying,
        Winner,
        Loser
    }
    
    public class GameState : MonoBehaviour
    {
    
        private GamePhases _gamePhases = GamePhases.SayHiToMum;
        private int _sceneNum = 0;
        private int _lastLevel = 0;
        private static GameState _instance;
      
        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("SoundManager Trying second Awake");
                Destroy(gameObject);
                return;
            }

            Debug.Log("GameState Awake");
            _instance = this as GameState;
            DontDestroyOnLoad(gameObject);
            SetupLastLevel();
        }

        public static GameState GetInstance()
        {
            return _instance;
        }

        public void ResetGameState()
        {
            this._sceneNum = 0;
            this.GamePhases = GamePhases.SayHiToMum;
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
        
        public GameState()
        {
            _gamePhases = GamePhases.SayHiToMum;
            _sceneNum = 0;
        }

        public int LastLevel
        {
            get => _lastLevel;
            set => _lastLevel = value;
        }

        public GamePhases GamePhases
        {
            get => _gamePhases;
            set => _gamePhases = value;
        }

        public int SceneNum
        {
            get => _sceneNum;
            set => _sceneNum = value;
        }
    }
}