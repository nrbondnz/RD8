using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            if (GameplayManager.Instance.GetLives() == 1 )
            {
                // just about to go to zero
                GameplayManager.Instance.RemoveLife();
                SceneManager.LoadScene("WinLoseMenu");
            }
            else
            {
                ResetScene();
            }
        }
    }

    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        GameplayManager.Instance.RemoveLife();
    }
}
