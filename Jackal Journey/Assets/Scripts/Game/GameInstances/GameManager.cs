using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public enum GameState { MainMenu, Gameplay, PlayerDeath, Pause }

public delegate void OnStateChangeHandler();
public class GameManager : MonoBehaviour
{
    [field: SerializeField] public UIHandler UIHandler { get; private set; } 
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public MenuManager MenuManager { get; private set; }

    public static GameManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<GameManager>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }
    protected static GameManager instance;

    public static GameManager Create()
    {
        GameObject gameManagerGameObject = new GameObject("GameManager");
        instance = gameManagerGameObject.AddComponent<GameManager>();

        return instance;
    }

 

    //public void RestartLevel()
    //{
    //    m_InPause = true;
    //    SwitchPauseState();
    //    RestartZone();
    //}

    void Update()
    {
        if (Player.GetComponent<InputHandler>().IsPausing)                         
            PauseOn();            
    }
    public void PauseOn()
    {
        Time.timeScale = 0;
        Player.GetComponent<InputHandler>().enabled = false;
        Player.GetComponent<CameraMovement>().enabled = false;
        UIHandler.enabled = true;
        MenuManager.PauseCanvas.SetActive(true);
        //----WIDOCZNOŒÆ MYSZY W MENU PAUZY--------------------------
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

    }
 

}

//--------------------STARY KOD NIE DZIALAJACY DO GAME MANAGERA--------------------------

    //protected GameManager() { }
    //private static GameManager instance = null;
    //public event OnStateChangeHandler OnStateChange;
    //public GameState gameState { get; private set; }

    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (GameManager.instance == null)
    //        {
    //            DontDestroyOnLoad(GameManager.instance);
    //            GameManager.instance = new GameManager();
    //        }
    //        return GameManager.instance;
    //    }

    //}

    //public void SetGameState(GameState state)
    //{
    //    this.gameState = state;
    //    OnStateChange();
    //}

    //public void OnApplicationQuit()
    //{
    //    GameManager.instance = null;
    //}

//}
//    public static GameManager Instance;

//    [field: SerializeField] public UIHandler UIHandler { get; private set; }
//    [field: SerializeField] public GameObject PauseCanvas { get; private set; }


//    public GameState State;

//    public static event Action<GameState> GameStateChanged;

//    private void Awake()
//    {
//        Instance = this;
//        //UIHandler.PauseEvent += Gameplay;

//    }

//    private void Start()
//    {
//        UpdateGameState(GameState.Gameplay);
//    }
//    public void UpdateGameState(GameState newState)
//    {
//        State = newState;

//        switch (newState)
//        {
//            case GameState.Gameplay:
//                Gameplay();
//                break;
//            case GameState.OpenedMenu:
//                Pause();
//                break;
//            case GameState.Loading:               
//                break;
//            default:
//                break;
//        }
//        GameStateChanged?.Invoke(newState);
//    }

//    private void Gameplay()
//    {
//        PauseCanvas.SetActive(true);
//        Time.timeScale = 0f;
//    }

//    private void Pause()
//    {

//    }
//}


//public enum GameState 
//{
//    Gameplay,
//    OpenedMenu,   
//    Loading   

//}