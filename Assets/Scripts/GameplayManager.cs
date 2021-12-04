using System;
using UnityEngine;
using UnityEngine.WSA;

namespace DefaultNamespace
{

    public class GameplayManager : MBSingleton<GameplayManager>
    {
        public GamePlay _gamePlay;

        
        public static Action<GamePlay> OnGamePlayChanged;

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
        OnGamePlayChanged?.Invoke(_gamePlay);
    }
        
        public int RemoveLife()
        {
            _gamePlay.removeLife();
            OnGamePlayChanged?.Invoke(_gamePlay);
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
            OnGamePlayChanged?.Invoke(_gamePlay);
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