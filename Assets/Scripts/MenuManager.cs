using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{


    public class MenuManager : Singleton<MenuManager>
    {
        // Start is called before the first frame update
        private GameManager _gameManager;

    
        
       

        private void Start()
        {
            _gameManager = GameManager.Instance;
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.SayHiToMum)
            {
                Debug.Log("Hi mum");
                //GameManager.getInstance().UpdateGameState(GameState.FirstScene);
            }
            else if (state == GameState.Winner)
            {
                // TODO change to gui with results
                _gameManager.UpdateGameState(GameState.SayHiToMum);
            } else if (state == GameState.Loser)
            {
                // TODO add loser GUI with results, could be same GUI as winner
            }
        }

        private void OnDestroy() => GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
}
