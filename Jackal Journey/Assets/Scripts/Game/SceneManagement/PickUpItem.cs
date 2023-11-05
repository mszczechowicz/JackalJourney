using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject savemanager;
    [field: SerializeField] public GameObject Player { get; set; }


    public void Interact()
    {
        savemanager.GetComponent<SaveManager>().LoadData();
        Player.GetComponent<InteractionHandler>().IsInteracting = false;
    }

}
