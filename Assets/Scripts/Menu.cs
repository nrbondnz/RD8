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
            if (GUI.Button(new Rect(10, 80, 80, 30), "Level 2"))
            {
                LevelTwo();
            }
            if (GUI.Button(new Rect(10, 120, 80, 30), "Level 3"))
            {
                LevelThree();
            }
            if (GUI.Button(new Rect(10, 160, 80, 30), "Quit"))
            {
                Quit();
            }

            GUI.EndGroup();
        }

        private void LevelThree()
        {
            Debug.Log("Menu: GameManager.Instance : " + GameManager.Instance);
            GameManager.Instance.UpdateGameState(GameState.ThirdScene);
            Debug.Log("Menu: New Game state : " + GameState.ThirdScene);
        }

        private void LevelTwo()
        {
            Debug.Log("Menu: GameManager.Instance : " + GameManager.Instance);
            GameManager.Instance.UpdateGameState(GameState.SecondScene);
            Debug.Log("Menu: New Game state : " + GameState.SecondScene);
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
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}