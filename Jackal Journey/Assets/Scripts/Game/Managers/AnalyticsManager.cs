using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AnalyticsManager : MonoBehaviour
{
    
    private const string ConsentKey = "DataConsent";
    private const string AgreeToCollectData  = "AgreeToCollectData";

    [SerializeField] GameObject ActiveButton;
    [SerializeField] GameObject AnalyticsDataCollectionWindow;
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject ActiveButtonContinue;
    async void Start()
    {

        await UnityServices.InitializeAsync();
        bool hasConsent = PlayerPrefs.GetInt(ConsentKey, 0) == 1;
        bool isCollectData = PlayerPrefs.GetInt(AgreeToCollectData, 0) == 1;

        if (isCollectData)
        {
            AnalyticsService.Instance.StartDataCollection();
        }
            
        if (!hasConsent)
        {

            AnalyticsDataCollectionWindow.SetActive(true);
            MainMenuUI.SetActive(false);

            ActiveButton.GetComponent<Button>().Select();
            
        }
        else
        {
            MainMenuUI.SetActive(true);
            AnalyticsDataCollectionWindow.SetActive(false);
            ActiveButtonContinue.GetComponent<Button>().Select();
       
        }

    }

    public void AcceptButton()
    {
        OptIn();
        AnalyticsDataCollectionWindow.SetActive(false);
        MainMenuUI.SetActive(true);
        PlayerPrefs.SetInt(ConsentKey, 1);
        PlayerPrefs.SetInt(AgreeToCollectData, 1);
        PlayerPrefs.Save();
    }
    public void DeclineButton()
    {

        AnalyticsDataCollectionWindow.SetActive(false);
        MainMenuUI.SetActive(true);
        PlayerPrefs.SetInt(ConsentKey, 1);
        PlayerPrefs.SetInt(AgreeToCollectData, 0);
        PlayerPrefs.Save();

    }

    public void OptIn()
    {
        AnalyticsService.Instance.StartDataCollection();
    }
    public void OptOut()
    {
        AnalyticsService.Instance.StopDataCollection();       
    }

    public void SwitchAalyticsOn()
    {
        OptIn();
        PlayerPrefs.SetInt(AgreeToCollectData, 1);
    }
    public void SwitchAalyticsOff()
    {
        OptOut();
        PlayerPrefs.SetInt(AgreeToCollectData, 0);
    }



    public void RequestDataDeletion()
    {
        AnalyticsService.Instance.RequestDataDeletion();
    }

   
    private void AskForConsent()
    {
        // Display a UI prompt asking the player for consent
        // For simplicity, let's assume a function DisplayConsentUI() is implemented elsewhere

      

        // Once the player provides consent, call the function SaveConsent() to store the consent state
        // For simplicity, let's assume a function SaveConsent() is implemented elsewhere
    }
    public void DelateConsent()
    {
      PlayerPrefs.DeleteKey(ConsentKey);
      
    }

    //TO DO: Application.OpenURL(AnalyticsService.Instance.PrivacyUrl);
}
