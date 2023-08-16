using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Managers
{
    public class UIScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        //private GamePlay _gamePlay;
        private GamePlayManager _gameplayManager = GamePlayManager.GetInstance();

        void Start()
        {
            Actions.OnGameStatusChanged += UpdateGameScore;
            UpdateGameScore(GamePlayManager.GetInstance().GameStatus);
        }

        private void OnDestroy()
        {
            Actions.OnGameStatusChanged -= UpdateGameScore;
        }

        private void Update()
        {
            //updateGameScore(GamePlayManager.Instance._gamePlay);
        }


        private void UpdateGameScore(GameStatus pGameStatus)
        {
            scoreText.text = "Lives : " + pGameStatus.Lives +
                             " Time: " + ConvertToMinsAndSecs(pGameStatus.TimeRemaining);
            if (pGameStatus.WaypointTimeRemaining > 0.0f)
            {
                scoreText.text += " Waypoint time left: " + 
                                  (int)pGameStatus.WaypointTimeRemaining;
            }
            else
            {
                scoreText.text += " Waypoint time left: --";
            }
        }

        private String ConvertToMinsAndSecs(float timeIn)
        {
            int mins = (int)(timeIn / 60.0);
            int secs = (int)(timeIn - (mins * 60));
            String secString = secs < 10 ? "0" + secs : secs.ToString();
            return mins + ":" + secString;
        }
        
        private String ConvertToSecs(float timeIn)
        {
            int secs = (int)timeIn;
            String secString = secs < 10 ? "0" + secs : secs.ToString();
            return secString;
        }
    }
}