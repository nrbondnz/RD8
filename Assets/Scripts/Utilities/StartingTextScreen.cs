using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities
{
    public class StartingTextScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headingForGameStart;

        [SerializeField] private Button easyButton;
        [SerializeField] private Button hardButton;
        [SerializeField] private Button impossibleButton;
        [SerializeField] private int startLevel = 1;

        void Awake()
        {
            if (!GamePlayManager.hasBootSceneRun())
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
            if ((GamePlayManager.GetInstance().GetGamePlay().Lives > 0) && (GamePlayManager.GetInstance().GetGamePlay().TimeRemaining > 0.0))
            {
                headingForGameStart.text = "Winner - Play again?";
            }
            else if ((GamePlayManager.GetInstance().GetGamePlay().Lives == 0) && (GamePlayManager.GetInstance().GetGamePlay().TimeRemaining > 0.0))
            {
                headingForGameStart.text = "Out of lives - Play again?";
            }
            else if ((GamePlayManager.GetInstance().GetGamePlay().Lives > 0) && (GamePlayManager.GetInstance().GetGamePlay().TimeRemaining <= 0.0))
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
            GameManager.GetInstance().UpdateGameState(GamePhases.GamePlaying,this.startLevel);
            GamePlayManager.GetInstance().InitGame(pGameDifficulty);
            Debug.Log("Menu: New Game state : " + GamePhases.GamePlaying + " " + GameStatusManager.GetInstance().SceneNum);
        }
    }
}