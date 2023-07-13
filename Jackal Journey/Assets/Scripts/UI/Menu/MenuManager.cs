using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [field: SerializeField] public UIHandler UIHandler { get; private set; }
    [field: SerializeField] public GameObject PauseCanvas { get; private set; }

    //[field: SerializeField] public GameObject Player{ get; private set; }

    private void Awake()
    {
       
        GameManager.GameStateChanged += GameplayState;
       
    }
    

    public void PauseState(GameState state)
    {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        //Player.GetComponent<CameraMovement>().enabled = false;
         
      

    }
    public void GameplayState(GameState state)
    {
        PauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        //Player.GetComponent<CameraMovement>().enabled = true;

       


    }

    public void BacktoMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


}
