using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake() => GameManager.OnGameStateChanged += OnGameStateChanged;

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.SayHiToMum)
        {
            Debug.Log("Hi mum");
        }
    }

    private void OnDestroy() => GameManager.OnGameStateChanged -= OnGameStateChanged;
}
