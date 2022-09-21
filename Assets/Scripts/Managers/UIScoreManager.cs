using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        //private GamePlay _gamePlay;
        private GamePlayManager _gameplayManager = GamePlayManager.GetInstance();

        void Start()
        {
            GamePlayManager.OnGamePlayChanged += updateGameScore;
            updateGameScore(GamePlayManager.GetInstance()._gamePlay);
        }

        private void OnDestroy()
        {
            GamePlayManager.OnGamePlayChanged -= updateGameScore;
        }

        private void Update()
        {
            //updateGameScore(GamePlayManager.Instance._gamePlay);
        }


        private void updateGameScore(GamePlay pGamePlay)
        {
            scoreText.text = "Lives : " + pGamePlay._lives + " Time: " + convertToMinsAndSecs(pGamePlay._timeRemaining);
        }

        private String convertToMinsAndSecs(float timeIn)
        {
            int mins = (int)(timeIn / 60.0);
            int secs = (int)(timeIn - (mins * 60));
            String secString = secs < 10 ? "0" + secs : secs.ToString();
            return mins + ":" + secString;
        }
    }
}