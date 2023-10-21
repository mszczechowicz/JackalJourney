using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractable
{
    [field: SerializeField] public GameObject Player { get; set; }

    public void Interact()
    {
       
       
       Destroy(gameObject);
        Debug.Log("Pickup: [ROCK]");
        Player.GetComponent<InteractionHandler>().IsInteracting = false;
    }

}
