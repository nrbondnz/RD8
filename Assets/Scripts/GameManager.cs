using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class GameManager : MBSingleton<GameManager>
    {
        private static GameManager _instance;
        //private GameplayManager _GameplayManager = GameplayManager.Instance;
        public GameState State;
        [Header("Sounds")] public AudioClip[] bounceSounds;

        private AudioSource _audioSource;

        public void PlayCollisionSound()
        {
            AudioClip collisionSound = bounceSounds[0];
            _audioSource.PlayOneShot(collisionSound);
        }
        

        public static event Action<GameState> OnGameStateChanged;

        private void Start()
        {
            //State = GameState.SayHiToMum;
            //SceneManager.LoadScene("Menu");
            //UpdateGameState(GameState.Winner);
            //GamePlayManager.Instance.InitGame(GameLevel.Easy);
        }


        public void UpdateGameState(GameState newState)
        {
            State = newState;
            Debug.Log("GameManager: new state : " + State);
            
            switch (newState)
            {
                
                case GameState.SayHiToMum:
                    SceneManager.LoadSceneAsync("Menu");
                    break;
                case GameState.FirstScene:
                    SceneManager.LoadSceneAsync(0);
                    break;
                case GameState.SecondScene:
                    SceneManager.LoadSceneAsync(1);
                    break;
                case GameState.ThirdScene:
                    SceneManager.LoadSceneAsync(2);
                    break;
                case GameState.Winner:
                    SceneManager.LoadSceneAsync("WinLoseMenu");
                    break;
                case GameState.Loser:
                    SceneManager.LoadSceneAsync("WinLoseMenu");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnGameStateChanged?.Invoke(newState);
        }

    }

    public enum GameState
    {
        SayHiToMum,
        FirstScene,
        SecondScene,
        ThirdScene,
        Winner,
        Loser
    }
}
