using System;
using Game;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{

    public class GamePlayManager : MonoBehaviour
    {
        public GameInfo GameInfo;

        private static GamePlayManager _instance;

        public GameInfo GetGamePlay()
        {
            return GameInfo;
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
            GameInfo.Lives = 0;
            GameInfo.Started = false;
            GameInfo.GameDifficulty = GameDifficulty.Easy;
            GameInfo.TimeRemaining = (float) 0.0;
            DontDestroyOnLoad(gameObject);
        }

        public static GamePlayManager GetInstance()
        {
            return _instance;
        }

        //public static Action<GamePlay> OnGamePlayChanged;

        public void Start()
        {
            GameInfo.Started = false;
        }

        public void InitGame(GameDifficulty pGameDifficulty)
        {
            GameInfo = new GameInfo();
            GameInfo.GameDifficulty = pGameDifficulty;
            if (GameInfo.GameDifficulty == GameDifficulty.Easy)
            {
                GameInfo.Lives = 15;
                GameInfo.TimeRemaining = 1000f;
            } else if (GameInfo.GameDifficulty == GameDifficulty.Hard)
            {
                GameInfo.Lives = 3;
                GameInfo.TimeRemaining = 200f;
            }
            else
            {
                GameInfo.Lives = 1;
                GameInfo.TimeRemaining = 100f;
            }
        Actions.OnGamePlayChanged?.Invoke(GameInfo);
    }
        
        public int RemoveLife()
        {
            GameInfo.RemoveLife();
            Actions.OnGamePlayChanged?.Invoke(GameInfo);
            return GameInfo.Lives;
        }
        
        public int GetLives()
        {
            return GameInfo.Lives;
        }

        public bool AnyLivesLeft()
        {
            return GameInfo.Lives == 0;
        }

        public void UpdateTimeRemaining()
        {
            GameInfo.TimeRemaining -= Time.deltaTime;
            Actions.OnGamePlayChanged?.Invoke(GameInfo);
            if (!AnyTimeLeft())
            {
                SceneManager.LoadScene("WinLoseMenu");
            }
        }

        public bool AnyTimeLeft()
        {
            return GameInfo.TimeRemaining > 0;
        }

        public GameDifficulty GetGameLevel()
        {
            return GameInfo.GameDifficulty;
        }
        
    }
    
    

    public enum GameDifficulty
    {
        Easy,
        Hard,
        Impossible
    }
}