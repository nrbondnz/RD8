using System.Collections;
using Managers;
using Managers.WaypointManagement;
using UnityEngine;

namespace Utilities
{


    public class WinningLevel : WaypointSubscriber
    {
        //private GameManager _gameManager;
        [SerializeField] private Material winningMaterial;
        [SerializeField] private bool instantWin = false;
        [SerializeField] private GameObject winningUI;
        private bool coroutineRunning = false;
       

        // Update is called once per frame
        void Update()
        {
            if (instantWin)
            {
                instantWin = false;
                StartCoroutine(WinningRoutine());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && (!coroutineRunning))
            {
                StartCoroutine(WinningRoutine());
            }
        }

        IEnumerator WinningRoutine()
        {
            coroutineRunning = true;
            GetComponent<MeshRenderer>().material = winningMaterial;
            winningUI.SetActive(true);
            Time.timeScale = 0.25f;
            yield return new WaitForSeconds(1.0f);
            Time.timeScale = 1f;
            //int currentSceneID = SceneManager.GetActiveScene().buildIndex;

            // TODO work out index max and rotate around scenes
            //SceneManager.LoadSceneAsync(currentSceneID == 0 ? 1 : currentSceneID == 1 ? 2 : 0);  
            GameStateManager gameStateManager = GameStateManager.Singleton;
            if (gameStateManager.GamePhase == GamePhase.GamePlaying)
            {
                if (gameStateManager.SceneNum < gameStateManager.LastLevel)
                {
                    gameStateManager.SceneNum++;
                    GameManager.Singleton.UpdateGameScene(GamePhase.GamePlaying, gameStateManager.SceneNum);
                }
                else
                {
                    GameManager.Singleton.UpdateGameScene(GamePhase.Winner, gameStateManager.SceneNum);
                }
            }
           

        }
    }
}
