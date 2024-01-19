using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private InteractionHandler Player;
    [SerializeField] private GameObject VFX;
    private Animator animator;
    public bool isOpen;


    public void Start()
    {   
        Player = FindAnyObjectByType<InteractionHandler>();
        animator = GetComponent<Animator>();
        isOpen = false;
    }
    public void Interact()
    {


        if (!isOpen)
        {
            
            Debug.Log("Chest Opened!");
            StartCoroutine(OpeningChest());
           

        }
        else
        {
            CloseChest();
            Debug.Log("Chest Closed!");
        }

       

    }

    private void CloseChest()
    {
        animator.SetTrigger("CloseChest");
        isOpen = !isOpen;
    }

    //private void OpenChest()
    //{
    //animator.SetTrigger("OpenChest");
    //    isOpen = !isOpen;
    //}

    private IEnumerator OpeningChest()
    {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();

        animator.SetTrigger("OpenChest");
        isOpen = !isOpen;
        yield return new WaitForSeconds(1f);
        this.gameObject.layer = LayerMask.NameToLayer("Ground");
        Destroy(VFX);
        yield return Player.IsInteracting = false;       
        CloseChest();
    }
}
