using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> GameStateChanged;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        UpdateGameState(GameState.Gameplay);
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Gameplay:
                Pause();
                break;
            case GameState.OpenedMenu:
                break;
            case GameState.Loading:               
                break;
            default:
                break;
        }
        GameStateChanged?.Invoke(newState);
    }

    private void Pause()
    {
        throw new NotImplementedException();
    }
}


public enum GameState 
{
    Gameplay,
    OpenedMenu,   
    Loading   

}