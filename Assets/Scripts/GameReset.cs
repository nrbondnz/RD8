using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReset : MBSingleton<GameReset>
{
    private void OnTriggerEnter(Collider other)
    {
        Terrain childTerrainObj = gameObject.GetComponentInChildren<Terrain>();
        if (other.CompareTag("Player") && ( childTerrainObj != null) && 
            (childTerrainObj.CompareTag("OutOfBounds")))
        {
            ResetAction();
        }
    }

    public void ResetAction()
    {
        if (GamePlayManager.Instance.GetLives() == 1 )
        {
            // just about to go to zero
            GamePlayManager.Instance.RemoveLife();
            SceneManager.LoadScene("WinLoseMenu");
        }
        else
        {
            ResetScene();
        }
    }

    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        GamePlayManager.Instance.RemoveLife();
    }
}
