using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        SceneLoader.Load(SceneLoader.Scene.LoadLevelPlayGround);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitButton()
    {
        Debug.Log("QuitApplication");
        Application.Quit();
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
}
