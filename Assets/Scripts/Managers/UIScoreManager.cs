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
            Actions.OnGamePlayChanged += UpdateGameScore;
            UpdateGameScore(GamePlayManager.GetInstance().GamePlay);
        }

        private void OnDestroy()
        {
            Actions.OnGamePlayChanged -= UpdateGameScore;
        }

        private void Update()
        {
            //updateGameScore(GamePlayManager.Instance._gamePlay);
        }


        private void UpdateGameScore(GamePlay pGamePlay)
        {
            scoreText.text = "Lives : " + pGamePlay.Lives + " Time: " + ConvertToMinsAndSecs(pGamePlay.TimeRemaining);
        }

        private String ConvertToMinsAndSecs(float timeIn)
        {
            int mins = (int)(timeIn / 60.0);
            int secs = (int)(timeIn - (mins * 60));
            String secString = secs < 10 ? "0" + secs : secs.ToString();
            return mins + ":" + secString;
        }
    }
}