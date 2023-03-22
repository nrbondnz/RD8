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
        
        // Start is called before the first frame update
        void Start()
        {
            easyButton.onClick.AddListener(EasyPressed);
            hardButton.onClick.AddListener(HardPressed);
            impossibleButton.onClick.AddListener(ImpossiblePressed);
        
            /*if ((!gamePlay._started))
        {
            gamePlay._started = true;
            headingForGameStart.text = "Want to play";
        } 
        else */ 
            if ((GamePlayManager.GetInstance().GetGameStatus().Lives > 0) && (GamePlayManager.GetInstance().GetGameStatus().TimeRemaining > 0.0))
            {
                headingForGameStart.text = "Winner - Play again?";
            }
            else if ((GamePlayManager.GetInstance().GetGameStatus().Lives == 0) && (GamePlayManager.GetInstance().GetGameStatus().TimeRemaining > 0.0))
            {
                headingForGameStart.text = "Out of lives - Play again?";
            }
            else if ((GamePlayManager.GetInstance().GetGameStatus().Lives > 0) && (GamePlayManager.GetInstance().GetGameStatus().TimeRemaining <= 0.0))
            {
                headingForGameStart.text = "Out of Time - Play again?";
            }
            else
            {
                headingForGameStart.text = "Want to Play?";
            }
        }


        public void EasyPressed()
        {
            StartGame(GameDifficulty.Easy);
        }
    
        public void HardPressed()
        {
            StartGame(GameDifficulty.Hard);
        }
    
        public void ImpossiblePressed()
        {
            StartGame(GameDifficulty.Impossible);
        }

        public void StartGame(GameDifficulty pGameDifficulty)
        {
            //start game scene
            //GameManager gameManager = GameManager.getInstance();
            Debug.Log("Menu: GameManager.Instance : " + GameManager.GetInstance());
            GameManager.GetInstance().UpdateGameScene(GamePhase.GamePlaying,this.startLevel);
            GamePlayManager.GetInstance().InitGame(pGameDifficulty);
            Debug.Log("Menu: New Game state : " + GamePhase.GamePlaying + " " + GameStateManager.GetInstance().SceneNum);
        }
    }
}