using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{

    public class GamePlayManager : MonoBehaviour
    {
        public GamePlay _gamePlay;

        private static GamePlayManager _instance;

        public GamePlay GetGamePlay()
        {
            return _gamePlay;
        }
        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("GamePlayManager Trying second Awake");
                Destroy(gameObject);
                return;
            }
            Debug.Log("GamePlayManager Awake");
            _instance = this;
            _gamePlay._lives = 0;
            _gamePlay._started = false;
            _gamePlay._gameLevel = GameLevel.Easy;
            _gamePlay._timeRemaining = (float) 0.0;
            DontDestroyOnLoad(gameObject);
        }

        public static GamePlayManager GetInstance()
        {
            return _instance;
        }

        //public static Action<GamePlay> OnGamePlayChanged;

        public void Start()
        {
            _gamePlay._started = false;
        }

        public void InitGame(GameLevel pGameLevel)
        {
            _gamePlay = new GamePlay();
            _gamePlay._gameLevel = pGameLevel;
            if (_gamePlay._gameLevel == GameLevel.Easy)
            {
                _gamePlay._lives = 15;
                _gamePlay._timeRemaining = 1000f;
            } else if (_gamePlay._gameLevel == GameLevel.Hard)
            {
                _gamePlay._lives = 3;
                _gamePlay._timeRemaining = 200f;
            }
            else
            {
                _gamePlay._lives = 1;
                _gamePlay._timeRemaining = 100f;
            }
        Actions.OnGamePlayChanged?.Invoke(_gamePlay);
    }
        
        public int RemoveLife()
        {
            _gamePlay.removeLife();
            Actions.OnGamePlayChanged?.Invoke(_gamePlay);
            return _gamePlay._lives;
        }
        
        public int GetLives()
        {
            return _gamePlay._lives;
        }

        public bool AnyLivesLeft()
        {
            return _gamePlay._lives == 0;
        }

        public void UpdateTimeRemaining()
        {
            _gamePlay._timeRemaining -= Time.deltaTime;
            Actions.OnGamePlayChanged?.Invoke(_gamePlay);
            if (!AnyTimeLeft())
            {
                SceneManager.LoadScene("WinLoseMenu");
            }
        }

        public bool AnyTimeLeft()
        {
            return _gamePlay._timeRemaining > 0;
        }

        public GameLevel GetGameLevel()
        {
            return _gamePlay._gameLevel;
        }
        
    }
    
    

    public enum GameLevel
    {
        Easy,
        Hard,
        Impossible
    }
}