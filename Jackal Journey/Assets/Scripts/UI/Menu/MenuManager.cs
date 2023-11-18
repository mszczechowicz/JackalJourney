using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [field: SerializeField] public UIHandler UIHandler { get; private set; }
    [field: SerializeField] public GameObject PauseMenuUI { get; private set; }

    [field: SerializeField] public GameObject TravelWindowCanvas { get; private set; }
   

    



    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (UIHandler.IsPausing)
            PauseOn();
    }

    #region PauseMenuButtons

    public void BacktoMainMenuButton()
    {
        Time.timeScale = 1f;       
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
    }
    public void PauseOff()
    {
        PauseMenuUI.SetActive(false);       
    }

    public void PauseOn()
    {
        PauseMenuUI.SetActive(true);
    }
    #endregion


}

