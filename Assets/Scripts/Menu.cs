using System;
using UnityEngine;
using System.Collections;

namespace DefaultNamespace
{


    public class Menu : Singleton<Menu>
    {
        GameManager gameManager;


       

        private void Start()
        {
            //gameManager = GameManager.Instance;
            gameManager = GameManager.Instance;
            GameManager.OnGameStateChanged += OnGameStateChanged;
            
            
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        public void OnGameStateChanged(GameState gameState)
        {
            Debug.Log("Menu: OnStateChange! : " + gameState);
        }


        public void OnGUI()
        {
            //menu layout
            GUI.BeginGroup(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 800));
            GUI.Box(new Rect(0, 0, 100, 200), "Menu");
            if (GUI.Button(new Rect(10, 40, 80, 30), "Start"))
            {
                StartGame();
            }

            if (GUI.Button(new Rect(10, 160, 80, 30), "Quit"))
            {
                Quit();
            }

            GUI.EndGroup();
        }


        public void StartGame()
        {
            //start game scene
            //GameManager gameManager = GameManager.getInstance();
            Debug.Log("Menu: GameManager.Instance : " + GameManager.Instance);
            GameManager.Instance.UpdateGameState(GameState.FirstScene);
            Debug.Log("Menu: New Game state : " + GameState.FirstScene);
        }


        public void Quit()
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }
}