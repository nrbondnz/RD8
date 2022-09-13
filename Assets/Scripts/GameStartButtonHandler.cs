using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using TMPro;

public class GameStartButtonHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headingForGameStart;
    GamePlay gamePlay = GamePlayManager.GetInstance()._gamePlay;
    
    // Start is called before the first frame update
    void Start()
    {
        /*if ((!gamePlay._started))
        {
            gamePlay._started = true;
            headingForGameStart.text = "Want to play";
        } 
        else */ 
        if ((gamePlay._lives > 0) && (gamePlay._timeRemaining > 0.0))
        {
            headingForGameStart.text = "Winner - Play again?";
        }
        else if ((gamePlay._lives == 0) && (gamePlay._timeRemaining > 0.0))
        {
            headingForGameStart.text = "Out of lives - Play again?";
        }
        else if ((gamePlay._lives > 0) && (gamePlay._timeRemaining <= 0.0))
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
        StartGame(GameLevel.Easy);
    }
    
    public void HardPressed()
    {
        StartGame(GameLevel.Hard);
    }
    
    public void ImpossiblePressed()
    {
        StartGame(GameLevel.Impossible);
    }
    
    public void StartGame(GameLevel pGameLevel)
    {
        //start game scene
        //GameManager gameManager = GameManager.getInstance();
        Debug.Log("Menu: GameManager.Instance : " + GameManager.GetInstance());
        GameManager.GetInstance().UpdateGameState(GameState.FirstScene);
        GamePlayManager.GetInstance().InitGame(pGameLevel);
        Debug.Log("Menu: New Game state : " + GameState.FirstScene);
    }
}
