using System;
using UnityEngine;
using System.Collections;

namespace DefaultNamespace
{


    public class Menu : MonoBehaviour{
    public void OnGUI()
        {
            //menu layout
            GUI.BeginGroup(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 800));
            GUI.Box(new Rect(0, 0, 100, 200), "Menu");
            if (GUI.Button(new Rect(10, 40, 80, 30), "Easy"))
            {
                StartGame(GameLevel.Easy);
            }
            if (GUI.Button(new Rect(10, 80, 80, 30), "Hard"))
            {
                StartGame(GameLevel.Hard);
            }
            if (GUI.Button(new Rect(10, 120, 80, 30), "Impossible"))
            {
                StartGame(GameLevel.Impossible);
            }
            if (GUI.Button(new Rect(10, 160, 80, 30), "Quit"))
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
            GamePlayManager.Instance.InitGame(pGameLevel);
        }


        public void Quit()
        {
            Debug.Log("Quit!");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}