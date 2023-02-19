using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameResetManager : MonoBehaviour
    {
        private static GameResetManager _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("GameReset Trying second Awake");
                Destroy(gameObject);
                return;
            }
            Debug.Log("GameReset Awake");
            _instance = this as GameResetManager;
            DontDestroyOnLoad(gameObject);
        }

        public static GameResetManager GetInstance()
        {
            return _instance;
        }

    
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
            if (GamePlayManager.GetInstance().GetLives() == 1 )
            {
                // just about to go to zero
                GamePlayManager.GetInstance().RemoveLife();
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
            GamePlayManager.GetInstance().RemoveLife();
        }
    }
}
