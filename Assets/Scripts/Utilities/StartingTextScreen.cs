using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities
{
    /// <summary>
    /// This displays what is the starting scene and the restart scene
    /// It first checks the game managers have been initialized, this is really so the developer can start from any scene
    /// Then the scene is setup connecting listeners to onClick for the action buttons
    /// It displays an appropriate message based on the GamePlayManager information
    /// - New game
    /// - Win
    /// - Loss
    /// - Out of time
    ///
    /// When the button is clicked the game is started initialising managers as required and setting the
    /// time and lives based on the difficulty button clicked
    /// </summary>
    public class StartingTextScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headingForGameStart;

        [SerializeField] private Button easyButton;
        [SerializeField] private Button hardButton;
        [SerializeField] private Button impossibleButton;
        [SerializeField] private int startLevel = 1;

        void Awake()
        {
            if (!GameManager.hasBootSceneRun())
            {
                SceneManager.LoadSceneAsync("Boot Dont Display");
            }
        }
        
        ///<summary>
        /// Start is called before the first frame update
        /// The buttons allocated on the screen are connected with listeners
        /// to methods for when the player clicks on them.
        /// Then it works out, based on the GamePlayManager state what message to display to the player.
        /// The message could be,
        /// - Play a game
        /// - You won
        /// - You lost
        /// - You ran out of time
        /// These will be displayed in the scene
        /// </summary>
        void Start()
        {
            easyButton.onClick.AddListener(EasyPressed);
            hardButton.onClick.AddListener(HardPressed);
            impossibleButton.onClick.AddListener(ImpossiblePressed);
            headingForGameStart.text = DecideOnTitleToDisplay();
        }

        /// <summary>
        /// Inspects the GamePlayManager state to decide on the title to display
        /// </summary>
        /// <returns>String</returns>
        private String DecideOnTitleToDisplay()
        {
            String titleString = null;
            if ((GamePlayManager.GetInstance().GetGameStatus().Lives > 0) &&
                (GamePlayManager.GetInstance().GetGameStatus().TimeRemaining > 0.0))
            {
                titleString = "Winner - Play again?";
            }
            else if ((GamePlayManager.GetInstance().GetGameStatus().Lives == 0) &&
                     (GamePlayManager.GetInstance().GetGameStatus().TimeRemaining > 0.0))
            {
                titleString = "Out of lives - Play again?";
            }
            else if ((GamePlayManager.GetInstance().GetGameStatus().Lives > 0) &&
                     (GamePlayManager.GetInstance().GetGameStatus().TimeRemaining <= 0.0))
            {
                titleString = "Out of Time - Play again?";
            }
            else
            {
                titleString = "Want to Play?";
            }

            return titleString;
        }


        public void EasyPressed()
        {
            StartGame(GameDifficulty.Easy);
        }
    
        public void HardPressed()
        {
            //startLevel = GameStateManager.GetInstance().LastLevel;
            StartGame(GameDifficulty.Hard);
            //startLevel = 1;
        }
    
        public void ImpossiblePressed()
        {
            StartGame(GameDifficulty.Impossible);
        }

        /// <summary>
        /// Sets up the start game state based on difficulty selected by the player
        /// The GameManager sets up the initial scene
        /// The GamePlayManager sets up the game based on the difficulty selected
        /// </summary>
        /// <param name="pGameDifficulty"></param>
        private void StartGame(GameDifficulty pGameDifficulty)
        {
            //start game scene
            //GameManager gameManager = GameManager.getInstance();
            Debug.Log("Menu: StartingTextScreen.StartGame.difficulty : " + pGameDifficulty + " startLevel : " + this.startLevel);
            GameManager.GetInstance().UpdateGameScene(GamePhase.GamePlaying,this.startLevel);
            GamePlayManager.GetInstance().InitGame(pGameDifficulty);
            Debug.Log("Menu: New Game state : " + GamePhase.GamePlaying + " " + GameStateManager.GetInstance().SceneNum);
        }
    }
}