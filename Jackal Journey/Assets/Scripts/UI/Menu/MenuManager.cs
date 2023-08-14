using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [field: SerializeField] public UIHandler UIHandler { get; private set; }
    [field: SerializeField] public GameObject PauseCanvas { get; private set; }

    [field: SerializeField] public GameObject TravelWindowCanvas { get; private set; }
    [field: SerializeField] public GameObject Player { get; private set; }



    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        Time.timeScale = 1;
        Player.GetComponent<InputHandler>().enabled = true;
        Player.GetComponent<CameraMovement>().enabled = true;
        UIHandler.enabled = false;
        Debug.Log("UIHANDLER OFF");
        PauseCanvas.SetActive(false);
        //----WIDOCZNOŒÆ MYSZY W MENU PAUZY----------------------
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    #endregion

    #region TravelWindowButtons
    //public void TravelYes()
    //{
    //    SceneManager.LoadScene("Gym");

    //}
    //public void TravelNo()
    //{
    //    TravelWindowCanvas.SetActive(false);
    //    Player.GetComponent<InteractionHandler>().IsInteracting = false;

    //}

    #endregion
}

//[Header("Mouse Cursor Settings")]
//public bool cursorLocked = true;
//public bool cursorInputForLook = true;

//private void OnApplicationFocus(bool hasFocus)
//{
//    SetCursorState(cursorLocked);
//}

//private void SetCursorState(bool newState)
//{
//    Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.Confined;
//}
////Widzialnoœækursora
//private void Start()
//{
//  //  UIHandler.PauseEvent += Pause;
//    Cursor.lockState = CursorLockMode.Locked;
//    Cursor.visible = false;
//}

//public void SceneSwitch()
//{
//    if (PauseCanvas.transform.GetChild(0).gameObject == true)
//        return;
//    PauseCanvas.transform.GetChild(0).gameObject.SetActive(true);


//}




//}
