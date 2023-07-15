using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField] public UIHandler UIHandler { get; private set; }
    [field: SerializeField] public GameObject PauseCanvas { get; private set; }


    public GameState State;

    public static event Action<GameState> GameStateChanged;

    private void Awake()
    {
        Instance = this;
        UIHandler.PauseEvent += Gameplay;
       
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
                Gameplay();
                break;
            case GameState.OpenedMenu:
                Pause();
                break;
            case GameState.Loading:               
                break;
            default:
                break;
        }
        GameStateChanged?.Invoke(newState);
    }

    private void Gameplay()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Pause()
    {
      
    }
}


public enum GameState 
{
    Gameplay,
    OpenedMenu,   
    Loading   

}