using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {

    
    public void Interact();

}

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] public Camera MainCamera;
    [SerializeField] private float InteractionRange;
    [SerializeField] public LayerMask layers;
    [SerializeField] private GameObject containerInteractionWindow;
    [SerializeField] public bool IsInteracting { get; set; } = false;



    // Update is called once per frame
    void Update()
    {
        if (GetInteractableObject() != null)
        {
            if (!IsInteracting)
            {
                Show();
            }
            else
            {
               Hide();
            }
        }
        else
        {           
                Hide();
            
        }

    }

    public IInteractable GetInteractableObject()
    {
        Ray r = new Ray(MainCamera.transform.position, MainCamera.transform.forward * InteractionRange);


        Debug.DrawRay(MainCamera.transform.position, MainCamera.transform.forward * InteractionRange, Color.blue, 1f);
        if (Physics.Raycast(r, out RaycastHit hitInfo, 10f, layers))
        {
            //Debug.DrawRay(stateMachine.MainCameraTransform.position, stateMachine.MainCameraTransform.forward, Color.red,100f);
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                return interactObj;
            }
        }
        return null;

    }
    private void Show()
    {
        containerInteractionWindow.SetActive(true);
    }

    private void Hide()
    {

        containerInteractionWindow?.SetActive(false);
    }

}
