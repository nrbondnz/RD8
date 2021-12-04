using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        private GamePlay _gamePlay;
        private GamePlayManager _gameplayManager = GamePlayManager.Instance;

        void Start()
        {
            GamePlayManager.OnGamePlayChanged += updateGameScore;
            updateGameScore(GamePlayManager.Instance._gamePlay);
        }

        private void OnDestroy()
        {
            GamePlayManager.OnGamePlayChanged -= updateGameScore;
        }


        private void updateGameScore(GamePlay pGamePlay)
        {
            _gamePlay = pGamePlay;
            scoreText.text = "Lives : " + _gamePlay._lives;
        }
    }
}