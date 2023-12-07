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
            if (other.CompareTag("Player"))
            {
                StartCoroutine(WinningRoutine());
            }
        }

        IEnumerator WinningRoutine()
        {
            GetComponent<MeshRenderer>().material = winningMaterial;
            winningUI.SetActive(true);
            Time.timeScale = 0.25f;
            yield return new WaitForSeconds(1f);
            Time.timeScale = 1f;
            //int currentSceneID = SceneManager.GetActiveScene().buildIndex;

            // TODO work out index max and rotate around scenes
            //SceneManager.LoadSceneAsync(currentSceneID == 0 ? 1 : currentSceneID == 1 ? 2 : 0);  
            //GameStateManager gameStateManager = GameStateManager.GetInstance();
            if (GameStateManager.Singleton.GamePhase == GamePhase.GamePlaying)
            {
                if (GameStateManager.Singleton.SceneNum < GameStateManager.Singleton.LastLevel)
                {
                    GameStateManager.Singleton.SceneNum++;
                    GameManager.Singleton.UpdateGameScene(GamePhase.GamePlaying, GameStateManager.Singleton.SceneNum);
                }
                else
                {
                    GameManager.Singleton.UpdateGameScene(GamePhase.Winner, GameStateManager.Singleton.SceneNum);
                }
            }
           

        }
    }
}
