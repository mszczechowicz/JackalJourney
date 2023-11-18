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

    [field: SerializeField] public GameObject PauseMenuUI{ get; private set; }

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

   
    //public void PauseOn()
    //{
    //    PauseMenuUI.SetActive(true);


    //    //Time.timeScale = 0;
    //    //Player.GetComponent<InputHandler>().enabled = false;
    //    //Player.GetComponent<CameraMovement>().enabled = false;
    //    //UIHandler.enabled = true;
    //    //MenuManager.PauseCanvas.SetActive(true);
    //    //----WIDOCZNOŒÆ MYSZY W MENU PAUZY--------------------------
    //    //Cursor.lockState = CursorLockMode.None;
    //    //Cursor.visible = true;

    //}
 

}

