using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{

    public class GamePlayManager : MonoBehaviour
    {
        public GameStatus GameStatus;

        private static GamePlayManager _instance;

        public static bool hasBootSceneRun()
        {
            return _instance != null;
        }
        
        public GameStatus GetGamePlay()
        {
            return GameStatus;
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
            GameStatus.Lives = 0;
            GameStatus.Started = false;
            GameStatus.GameDifficulty = GameDifficulty.Easy;
            GameStatus.TimeRemaining = (float) 0.0;
            DontDestroyOnLoad(gameObject);
        }

        public static GamePlayManager GetInstance()
        {
            return _instance;
        }

        //public static Action<GamePlay> OnGamePlayChanged;

        public void Start()
        {
            GameStatus.Started = false;
        }

        public void InitGame(GameDifficulty pGameDifficulty)
        {
            GameStatus = new GameStatus();
            GameStatus.GameDifficulty = pGameDifficulty;
            if (GameStatus.GameDifficulty == GameDifficulty.Easy)
            {
                GameStatus.Lives = 15;
                GameStatus.TimeRemaining = 1000f;
            } else if (GameStatus.GameDifficulty == GameDifficulty.Hard)
            {
                GameStatus.Lives = 3;
                GameStatus.TimeRemaining = 200f;
            }
            else
            {
                GameStatus.Lives = 1;
                GameStatus.TimeRemaining = 100f;
            }
        Actions.OnGamePlayChanged?.Invoke(GameStatus);
    }
        
        public int RemoveLife()
        {
            GameStatus.RemoveLife();
            Actions.OnGamePlayChanged?.Invoke(GameStatus);
            return GameStatus.Lives;
        }
        
        public int GetLives()
        {
            return GameStatus.Lives;
        }

        public bool AnyLivesLeft()
        {
            return GameStatus.Lives == 0;
        }

        public void UpdateTimeRemaining()
        {
            GameStatus.TimeRemaining -= Time.deltaTime;
            Actions.OnGamePlayChanged?.Invoke(GameStatus);
            if (!AnyTimeLeft())
            {
                SceneManager.LoadScene("WinLoseMenu");
            }
        }

        public bool AnyTimeLeft()
        {
            return GameStatus.TimeRemaining > 0;
        }

        public GameDifficulty GetGameLevel()
        {
            return GameStatus.GameDifficulty;
        }
        
    }
    
    

    public enum GameDifficulty
    {
        Easy,
        Hard,
        Impossible
    }
}