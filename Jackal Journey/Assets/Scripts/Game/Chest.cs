using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject Player;
    public void Interact()
    {
        Debug.Log("Chest Opened!");

       // Player.InteractionHandler.Isinteracting = false;

    }

   
}
