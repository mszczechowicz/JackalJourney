using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Quest : MonoBehaviour, IInteractable
{
    [SerializeField] Dialogue dialoguepopup;
    private InteractionHandler Player;
    private bool isReading;
    public void Interact()
    {
        if (!isReading)
        {

            Debug.Log("Chest Opened!");
            StartCoroutine(ReadingQuote());


        }

        else
        {
            CloseDialog();
           
        }
        
        





    }
    public void Start()
    {
        Player = FindAnyObjectByType<InteractionHandler>();
        
    }
    private void CloseDialog()
    {

        isReading = !isReading;
    }
    private IEnumerator ReadingQuote()
    {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();
        dialoguepopup.ShowDialog();

        isReading = !isReading;
        yield return new WaitForSeconds(1.5f);


        yield return Player.IsInteracting = false;
        CloseDialog();
    }
}
