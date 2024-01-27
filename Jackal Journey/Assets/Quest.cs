using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Quest : MonoBehaviour, IInteractable
{
    [SerializeField] Dialogue dialoguepopup;
    private InteractionHandler Player;
    public void Interact()
    {
        dialoguepopup.ShowDialog();
        Player.IsInteracting = false;

    }
    public void Start()
    {
        Player = FindAnyObjectByType<InteractionHandler>();
        
    }
}
