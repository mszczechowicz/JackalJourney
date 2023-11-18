using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
//    //Tutaj musimy wprowadziæ kod który obsluzy nam wczytywanie sceny z ostatniego zapisu gry
//    [SerializeField] private string SavedSceneToLoadInGame;


//    public void PlayButton()
//    {
//        //Tutaj musimy wprowadziæ kod który obsluzy nam wczytywanie sceny z ostatniego zapisu gry
//        //SceneLoader.SceneToLoad(SavedSceneToLoadInGame);
//        SceneManager.LoadScene(SavedSceneToLoadInGame);
//    }

//    public void QuitButton()
//    {
//#if UNITY_EDITOR
//        EditorApplication.ExitPlaymode();
//#endif

//        Debug.Log("QuitApplication");
//        Application.Quit();

//    }
//    public void BackToMain_Buttons()
//    {
        
//    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }


    //--------------------------------------------------------------------





    SavingWrapper savingWrapper;

    [SerializeField] TMP_InputField newGameNameField;
    


    private void Awake()
    {
        savingWrapper = GetSavingWrapper();
        
      

        
    }

    private SavingWrapper GetSavingWrapper()
    {
        return FindObjectOfType<SavingWrapper>();
    }

    public void ContinueGame()
    {
        savingWrapper.ContinueGame();
    }

    public void NewGame()
    {
        savingWrapper.NewGame(newGameNameField.text);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }


}
