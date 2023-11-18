using TMPro;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class UIScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        //private GamePlay _gamePlay;
        //private GamePlayManager _gameplayManager = GamePlayManager.GetInstance();

        /// <summary>
        /// The subscription to OnGameStatusChanged will cause this screen to update the text on screen during the game
        /// </summary>
        void Start()
        {
            UpdateGameScore(GamePlayManager.GetInstance().GameStatus);
        }

        private void OnEnable()
        {
            Actions.OnGameStatusChanged += UpdateGameScore;
        }

        private void OnDisable()
        {
            Actions.OnGameStatusChanged -= UpdateGameScore;
        }

        /// <summary>
        /// Displays the game score info top left
        /// </summary>
        /// <param name="pGameStatus"></param>
        private void UpdateGameScore(GameStatus pGameStatus)
        {
            scoreText.text = "Lives : " + pGameStatus.Lives + "<br>" +
                             "Time: " + GameObjectUtilities.ConvertToMinsAndSecs(pGameStatus.TimeRemaining);
            if (pGameStatus.WaypointTimeRemaining > 0.0f)
            {
                scoreText.text += "<br>" +"Waypoint time : " + 
                                  (int)pGameStatus.WaypointTimeRemaining;
            }
            else
            {
                scoreText.text += "<br>" +"Waypoint time : --";
            }
        }
    }
}