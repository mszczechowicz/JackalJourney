using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MoveItems : MonoBehaviour, IInteractable
{

    private Rigidbody rigidbody;
    private float speed = 2f;
    [SerializeField] GameObject cameraGameObject;
    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
       
    }
    public void Interact()
    {

        Debug.Log("Przenosisz przedmiot");
        rigidbody.AddForce(cameraGameObject.transform.forward * speed);

    }
    
}
