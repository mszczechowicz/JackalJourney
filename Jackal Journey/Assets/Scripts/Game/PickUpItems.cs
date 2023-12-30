using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour, IInteractable
{
    public void Interact()
    {

        Debug.Log("Podnios³eœ przedmiot");
        Destroy(this.gameObject);
    }
}
