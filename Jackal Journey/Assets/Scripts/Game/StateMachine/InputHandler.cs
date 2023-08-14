using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, Controls.IPlayerActions
{
    public bool IsAttacking { get; private set; }
    public bool IsInteracting { get; private set; }

    public bool IsPausing { get; private set; }

    public Vector2 MovementValue { get; private set; }

    public Vector2 CameraValue { get; private set; }

    public event Action DashEvent;

    public event Action JumpEvent;
   

    private Controls controls;

    

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed){return;}

        DashEvent?.Invoke();

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        CameraValue = context.ReadValue<Vector2>();
    }

    


    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        { 
            IsAttacking = true;
        }

        else if (context.canceled)
        {
            IsAttacking = false;
        }

        
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsInteracting = true;
        }

        else if (context.canceled)
        {
            IsInteracting = false;
        }

    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsPausing = true;
            Debug.Log("PAUSE On");
        }

        else if (context.canceled)
        {
            IsPausing = false;
            Debug.Log("PAUSE Off");
        }

    }


}
