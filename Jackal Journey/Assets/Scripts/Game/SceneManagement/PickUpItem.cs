using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Services.Analytics;
using UnityEngine;
#if ENABLE_CLOUD_SERVICES_ANALYTICS
using UnityEngine.Analytics;
#endif

public class PickUpItem : MonoBehaviour, IInteractable
{
    
    [field: SerializeField] public GameObject Player { get; set; }


    public void Interact()
    {
        int totalPotions = 5;
        int totalCoins = 100;
#if ENABLE_CLOUD_SERVICES_ANALYTICS
        Debug.Log("GamveOverEventAnalitics");
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            { "potions", totalPotions },
            { "coins", totalCoins }
        });
        AnalyticsService.Instance.CustomData("gameOver");
#endif
        Player.GetComponent<InteractionHandler>().IsInteracting = false;
    }

}
