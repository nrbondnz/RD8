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
        private GameplayManager _gameplayManager = GameplayManager.Instance;

        void Start()
        {
            GameplayManager.OnGamePlayChanged += updateGameScore;
            updateGameScore(GameplayManager.Instance._gamePlay);
        }

        private void OnDestroy()
        {
            GameplayManager.OnGamePlayChanged -= updateGameScore;
        }


        private void updateGameScore(GamePlay pGamePlay)
        {
            _gamePlay = pGamePlay;
            scoreText.text = "Lives : " + _gamePlay._lives;
        }
    }
}