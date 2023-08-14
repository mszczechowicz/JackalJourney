using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIHandler : MonoBehaviour, Controls.IUIActions
{

    private Controls controls;

    public bool PausingOf { get; private set; }

    void Start()
    {
        controls = new Controls();
        controls.UI.SetCallbacks(this);

        controls.UI.Enable();
    }
    private void OnDestroy()
    {
        controls.UI.Disable();
    }

    


    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PausingOf = true;
        }

        else if (context.canceled)
        {
            PausingOf = false;
        }

    }

}
