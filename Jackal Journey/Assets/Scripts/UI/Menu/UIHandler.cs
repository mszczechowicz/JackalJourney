using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIHandler : MonoBehaviour, Controls.IUIActions
{

    private Controls controls;

    public event Action PauseEvent;

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

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        PauseEvent?.Invoke();
    }

}
