using System.Collections;
using Managers;
using UnityEngine;

namespace Game
{


    public class WinningLevel : MonoBehaviour
    {
        //private GameManager _gameManager;
        [SerializeField] private Material winningMaterial;
        [SerializeField] private bool instantWin = false;
        [SerializeField] private GameObject winningUI;

        // Start is called before the first frame update
        void Start()
        {
            
        }
        
        

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
            GameState gameState = GameState.GetInstance();
            if (gameState.GamePhases == GamePhases.GamePlaying)
            {
                if (gameState.SceneNum < gameState.LastLevel)
                {
                    gameState.SceneNum++;
                    GameManager.GetInstance().UpdateGameState(GamePhases.GamePlaying, gameState.SceneNum);
                }
                else
                {
                    GameManager.GetInstance().UpdateGameState(GamePhases.Winner, gameState.SceneNum);
                }
            }
           

        }
    }
}
