using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{

    /// <summary>
    /// Creates a single instance GamePlayManager controlling GameStatus
    /// </summary>
    public class GamePlayManager : MonoBehaviour
    {
        public GameStatus GameStatus;

        private static GamePlayManager _instance;

    
        
        /// <summary>
        /// Returns the GameStatus
        /// </summary>
        /// <returns>GameStatus</returns>
        public GameStatus GetGameStatus()
        {
            return GameStatus;
        }
        
        /// <summary>
        /// Sets up the Singleton instance for GamePlayManager
        /// </summary>
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

        /// <summary>
        /// Returns the Singleton instance of GamePlayManager
        /// </summary>
        /// <returns>GamePlayManager</returns>
        public static GamePlayManager GetInstance()
        {
            return _instance;
        }

        //public static Action<GamePlay> OnGamePlayChanged;

        /// <summary>
        /// Start the GamePlayManager in the GameStatus.Started = false state
        /// </summary>
        public void Start()
        {
            GameStatus.Started = false;
        }

        /// <summary>
        /// Initializes the GameStatus of the game according to GameDifficulty parameter
        /// And informs subscribers of the state change
        /// </summary>
        /// <param name="pGameDifficulty">GameDifficulty</param>
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
        Actions.OnGameStatusChanged?.Invoke(GameStatus);
    }
        /// <summary>
        /// Removes a life from GameStatus and returns the number of lives remaining
        /// </summary>
        /// <returns>int</returns>
        public int RemoveLife()
        {
            GameStatus.RemoveLife();
            Actions.OnGameStatusChanged?.Invoke(GameStatus);
            return GameStatus.Lives;
        }
        
        /// <summary>
        /// Get the number of lives left
        /// </summary>
        /// <returns>int</returns>
        public int GetLives()
        {
            return GameStatus.Lives;
        }

        /// <summary>
        /// Are any lives remaining
        /// </summary>
        /// <returns>bool</returns>
        public bool AnyLivesLeft()
        {
            return GameStatus.Lives == 0;
        }

        /// <summary>
        /// Updates the time remaining
        /// Tells subscribers if the GameStatus has changed
        /// If out of time it causes the WinLoseMenu to be displayed
        /// </summary>
        public void UpdateTimeRemaining()
        {
            GameStatus.TimeRemaining -= Time.deltaTime;
            Actions.OnGameStatusChanged?.Invoke(GameStatus);
            if (!AnyTimeLeft())
            {
                SceneManager.LoadScene("WinLoseMenu");
            }
        }

        /// <summary>
        /// Checks the GameStatus to see if any time is left
        /// </summary>
        /// <returns>bool</returns>
        private bool AnyTimeLeft()
        {
            return GameStatus.TimeRemaining > 0;
        }

        /// <summary>
        /// Get GameDifficulty
        /// </summary>
        /// <returns>GameDifficulty</returns>
        public GameDifficulty GetGameDifficulty()
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