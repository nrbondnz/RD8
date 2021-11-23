using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            ResetScene();
        }
    }

    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(currentScene.name);
    }
}
