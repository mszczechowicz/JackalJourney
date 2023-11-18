using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] Transform contentRoot;
    [SerializeField] GameObject buttonPrefab;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loadMenu;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject escHint;

    public void OnEnable()
    {
        SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
        if (savingWrapper == null) return;
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }
        foreach (string save in savingWrapper.ListSaves())
        {
            GameObject buttonInstance = Instantiate(buttonPrefab, contentRoot);
            TMP_Text textComp = buttonInstance.GetComponentInChildren<TMP_Text>();
            textComp.text = save;
            Button button = buttonInstance.GetComponentInChildren<Button>();
            button.onClick.AddListener(() =>
            {
                savingWrapper.LoadGame(save);
            });

          

            EventTrigger eventTrigger = buttonInstance.GetComponent<EventTrigger>();
            EventTrigger.Entry cancelEntrybutton = new EventTrigger.Entry();
            cancelEntrybutton.eventID = EventTriggerType.Cancel;
            
            cancelEntrybutton.callback.AddListener((data) => OnContentRootCancel() );
            eventTrigger.triggers.Add(cancelEntrybutton);

          

          

        }
        Debug.Log("Selected object: " + EventSystem.current.currentSelectedGameObject);
        GameObject firstButton = contentRoot.GetChild(0).gameObject;       
        firstButton.GetComponent<Button>().Select();
       
        Debug.Log("Selected object: " + EventSystem.current.currentSelectedGameObject);

      

    }
    

    public void OnContentRootCancel()
    {
        mainMenu.SetActive(true);
        loadMenu.SetActive(false);

        continueButton.GetComponent<Button>().Select();
        escHint.SetActive(false);
        Debug.Log(" OnContentRootCancel()");
        

    }
}
