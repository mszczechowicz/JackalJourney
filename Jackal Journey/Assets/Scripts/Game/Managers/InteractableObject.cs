using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject savemanager;
    [field: SerializeField] public GameObject Player { get; set; }


    public void Interact()
    {
      
        Player.GetComponent<InteractionHandler>().IsInteracting = false;
    }
}
