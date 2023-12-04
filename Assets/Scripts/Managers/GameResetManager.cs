using System;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{
    /// <summary>
    /// Resets the game after losing a life or running out of time with this singleton
    /// </summary>
    public class GameResetManager : MonoBehaviour
    {
        private static GameResetManager _instance;
        private PlayerInputState _playerInputState;

        private void UpdatePlayerInputState(PlayerInputState pPlayerInputState)
        {
            _playerInputState = pPlayerInputState;
        }
        
        public void OnEnable()
        {
            Actions.OnPlayerInput += UpdatePlayerInputState;
        }

        public void OnDisable()
        {
            Actions.OnPlayerInput -= UpdatePlayerInputState;
        }


        /// <summary>
        /// Initializes a Singleton of the GameResetManager class
        /// </summary>
        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("GameResetManager Trying second Awake");
                Destroy(gameObject);
                return;
            }
            Debug.Log("GameResetManager Awake");
            _instance = this as GameResetManager;
            DontDestroyOnLoad(gameObject);
            if (!GameManager.hasBootSceneRun())
            {
                SceneManager.LoadSceneAsync("Boot Dont Display");
            }
        }

        /// <summary>
        /// Gets the singleton of GameResetManager
        /// </summary>
        /// <returns></returns>
        public static GameResetManager GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// Listens for collisions with a collider object setup in GameReset
        /// TODO needs more TLC as simplistic
        /// then if found calls the RemoveLiveAndResetScene
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            //Terrain childTerrainObj = gameObject.GetComponentInChildren<Terrain>();
            if (other.CompareTag("Player")) /*&& ( childTerrainObj != null) && 
                (childTerrainObj.CompareTag("OutOfBounds")))*/
            {
                RemoveLifeAndResetScene();
            }
        }

        /// <summary>
        /// Resets the scene if still lives left else the game has ended so update the
        /// GamePlayManager and loads the WinLoseMenu
        /// </summary>
        public void RemoveLifeAndResetScene()
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

        /// <summary>
        /// Resets the scene and removes a life from the GamePlayManager
        /// </summary>
        public void ResetScene()
        
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
            //Player.OnScreenPlayerUpdate onScreenPlayerUpdate = Player.OnScreenPlayerUpdate.GetInstance();
            //onScreenPlayerUpdate.HozInput = 0.0f;
            //onScreenPlayerUpdate.VertInput = 0.0f;
            PlayerInputState playerInputState = new PlayerInputState(0.0f, 0.0f, false);
            Actions.OnPlayerInput(playerInputState);
            GamePlayManager.GetInstance().RemoveLife();
            
        }
    }
}
