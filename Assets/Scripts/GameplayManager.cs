using UnityEngine;
using UnityEngine.WSA;

namespace DefaultNamespace
{
    public class GameplayManager : Singleton<GameplayManager>
    {
        private int _lives;
        private GameLevel _gameLevel;
        private float _timeRemaining;

        public void InitGame(GameLevel pGameLevel)
        {
            _gameLevel = pGameLevel;
            if (_gameLevel == GameLevel.Easy)
            {
                _lives = 15;
                _timeRemaining = 1000f;
            } else if (_gameLevel == GameLevel.Hard)
            {
                _lives = 3;
                _timeRemaining = 200f;
            }
            else
            {
                _lives = 1;
                _timeRemaining = 100f;
            }
        }
        
        public int RemoveLife()
        {
            _lives -= 1;
            return _lives;
        }
        
        public int GetLives()
        {
            return _lives;
        }

        public bool AnyLivesLeft()
        {
            return _lives > 0;
        }

        public void UpdateTimeRemaining()
        {
            _timeRemaining -= Time.deltaTime;
        }

        public bool AnyTimeLeft()
        {
            return _timeRemaining > 0;
        }

        public GameLevel GetGameLevel()
        {
            return this._gameLevel;
        }
        
    }
    
    

    public enum GameLevel
    {
        Easy,
        Hard,
        Impossible
    }
}