using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //Tutaj musimy wprowadziæ kod który obsluzy nam wczytywanie sceny z ostatniego zapisu gry
    [SerializeField] private string SavedSceneToLoadInGame;


    public void PlayButton()
    {
        //Tutaj musimy wprowadziæ kod który obsluzy nam wczytywanie sceny z ostatniego zapisu gry
        SceneLoader.SceneToLoad(SavedSceneToLoadInGame);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif

        Debug.Log("QuitApplication");
        Application.Quit();

    }
    public void BackToMain_Buttons()
    {
        
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
}
