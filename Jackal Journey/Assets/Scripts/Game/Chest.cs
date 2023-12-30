using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject Player;
    private Animator animator;
    public bool isOpen;


    public void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
    }
    public  void Interact()
    {


        if (!isOpen)
        {
            OpenChest();
            Debug.Log("Chest Opened!");

        }
        else
        {
            CloseChest();
            Debug.Log("Chest Closed!");
        }

        // Player.InteractionHandler.Isinteracting = false;

    }

    private void CloseChest()
    {
        animator.SetTrigger("CloseChest");
        isOpen = !isOpen;
    }

    private void OpenChest()
    {
        animator.SetTrigger("OpenChest");
        isOpen= !isOpen;
    }
   
}
