using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour, IInteractable
{
    public void Interact()
    {

        Debug.Log("Podniosłeś przedmiot");
        Destroy(this.gameObject);
    }
}
