using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour, IInteractable
{
    public void Interact()
    {

        Debug.Log("Podnios�e� przedmiot");
        Destroy(this.gameObject);
    }
}
