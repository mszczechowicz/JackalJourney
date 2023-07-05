using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public UIHandler UIHandler { get; private set; }
    [field: SerializeField] public GameObject PauseCanvas { get; private set; }

    [field: SerializeField] public GameObject Player{ get; private set; }

    private void Awake()
    {
        UIHandler.PauseEvent += Pause;
       
    }
    

    public void Pause()
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        Player.GetComponent<CameraMovement>().enabled = false;
        UIHandler.PauseEvent -= Pause;
        UIHandler.PauseEvent += Resume;

    }
    public void Resume()
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        Player.GetComponent<CameraMovement>().enabled = true;
        UIHandler.PauseEvent -= Resume;
        UIHandler.PauseEvent += Pause;



    }

    public void BacktoMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


}
