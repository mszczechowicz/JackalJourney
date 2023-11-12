using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] UIHandler UIHandler;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        if (Player == null) return;
        Time.timeScale = 0;
        Player.GetComponent<InputHandler>().enabled = false;
        Player.GetComponent<CameraMovement>().enabled = false;
        UIHandler.enabled = false;
    }

    private void OnDisable()
    {
        if (Player == null) return;
        Time.timeScale = 1;
        Player.GetComponent<InputHandler>().enabled = true;
        Player.GetComponent<CameraMovement>().enabled = true;
        UIHandler.enabled = true;
    }

    public void Save()
    {
        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
        savingWrapper.Save();
    }

    public void SaveAndQuit()
    {
        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
        savingWrapper.Save();
        savingWrapper.LoadMenu();
    }

}
