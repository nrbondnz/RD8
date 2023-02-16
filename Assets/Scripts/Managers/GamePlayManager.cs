using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{

    public class GamePlayManager : MonoBehaviour
    {
        public GamePlay GamePlay;

        private static GamePlayManager _instance;

        public GamePlay GetGamePlay()
        {
            return GamePlay;
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
            GamePlay.Lives = 0;
            GamePlay.Started = false;
            GamePlay.GameLevel = GameLevel.Easy;
            GamePlay.TimeRemaining = (float) 0.0;
            DontDestroyOnLoad(gameObject);
        }

        public static GamePlayManager GetInstance()
        {
            return _instance;
        }

        //public static Action<GamePlay> OnGamePlayChanged;

        public void Start()
        {
            GamePlay.Started = false;
        }

        public void InitGame(GameLevel pGameLevel)
        {
            GamePlay = new GamePlay();
            GamePlay.GameLevel = pGameLevel;
            if (GamePlay.GameLevel == GameLevel.Easy)
            {
                GamePlay.Lives = 15;
                GamePlay.TimeRemaining = 1000f;
            } else if (GamePlay.GameLevel == GameLevel.Hard)
            {
                GamePlay.Lives = 3;
                GamePlay.TimeRemaining = 200f;
            }
            else
            {
                GamePlay.Lives = 1;
                GamePlay.TimeRemaining = 100f;
            }
        Actions.OnGamePlayChanged?.Invoke(GamePlay);
    }
        
        public int RemoveLife()
        {
            GamePlay.RemoveLife();
            Actions.OnGamePlayChanged?.Invoke(GamePlay);
            return GamePlay.Lives;
        }
        
        public int GetLives()
        {
            return GamePlay.Lives;
        }

        public bool AnyLivesLeft()
        {
            return GamePlay.Lives == 0;
        }

        public void UpdateTimeRemaining()
        {
            GamePlay.TimeRemaining -= Time.deltaTime;
            Actions.OnGamePlayChanged?.Invoke(GamePlay);
            if (!AnyTimeLeft())
            {
                SceneManager.LoadScene("WinLoseMenu");
            }
        }

        public bool AnyTimeLeft()
        {
            return GamePlay.TimeRemaining > 0;
        }

        public GameLevel GetGameLevel()
        {
            return GamePlay.GameLevel;
        }
        
    }
    
    

    public enum GameLevel
    {
        Easy,
        Hard,
        Impossible
    }
}