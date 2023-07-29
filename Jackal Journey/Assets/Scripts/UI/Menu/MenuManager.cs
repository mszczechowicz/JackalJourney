using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        UIHandler.PauseEvent += Resume;
        Cursor.visible = false;
    }


    public void Pause()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        Player.GetComponent<CameraMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIHandler.PauseEvent -= Pause;
        UIHandler.PauseEvent += Resume;


    }
    public void Resume()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        Player.GetComponent<CameraMovement>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UIHandler.PauseEvent -= Resume;
        UIHandler.PauseEvent += Pause;

       

    }

    public void BacktoMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
    //Widzialnoœækursora
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //public void SceneSwitch()
    //{
    //    if (PauseCanvas.transform.GetChild(0).gameObject == true)
    //        return;
    //    PauseCanvas.transform.GetChild(0).gameObject.SetActive(true);

       
    //}

    public void TravelYes()
    {
        SceneManager.LoadScene("Gym");

    }
    public void TravelNo()
    {
      TravelWindowCanvas.SetActive(false);

    }


}
