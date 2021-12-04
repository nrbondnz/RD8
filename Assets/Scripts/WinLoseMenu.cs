using System;
using UnityEngine;
using System.Collections;

namespace DefaultNamespace
{


    public class WinLoseMenu : MonoBehaviour
    {
        
        public void OnGUI()
        {
            GamePlay gamePlay = GameplayManager.Instance._gamePlay;
            //menu layout
            GUI.BeginGroup(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 120, 900));
            if (gamePlay._lives > 0)
            {
                GUI.Box(new Rect(0, 0, 100, 300), "Winner");
            }
            else
            {
                GUI.Box(new Rect(0, 0, 100, 300), "Lost");
            }
            GUI.Box(new Rect(10, 40, 80, 30), "Play again?");
            if (GUI.Button(new Rect(10, 80, 80, 30), "Easy"))
            {
                StartGame(GameLevel.Easy);
            }
            if (GUI.Button(new Rect(10, 120, 80, 30), "Hard"))
            {
                StartGame(GameLevel.Hard);
            }
            if (GUI.Button(new Rect(10, 160, 80, 30), "Impossible"))
            {
                StartGame(GameLevel.Impossible);
            }
            if (GUI.Button(new Rect(10, 200, 80, 30), "Quit"))
            {
                Quit();
            }

            GUI.EndGroup();
        }

        


        public void StartGame(GameLevel pGameLevel)
        {
            //start game scene
            //GameManager gameManager = GameManager.getInstance();
            Debug.Log("Menu: GameManager.Instance : " + GameManager.Instance);
            GameManager.Instance.UpdateGameState(GameState.FirstScene);
            GameplayManager.Instance.InitGame(pGameLevel);
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