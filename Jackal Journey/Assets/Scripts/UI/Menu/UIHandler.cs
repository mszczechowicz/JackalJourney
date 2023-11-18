using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIHandler : MonoBehaviour, Controls.IUIActions
{
    public bool IsPausing { get; private set; }

    public Controls controls;

    private void Start()
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
            IsPausing = true;
            Debug.Log("PauseButton");
        }

        else if (context.canceled)
        {
            IsPausing = false;

        }
    }


    //public static UIHandler Instance
    //{
    //    get
    //    {
    //        if (instance != null)
    //            return instance;

    //        instance = FindObjectOfType<UIHandler>();

    //        if (instance != null)
    //            return instance;

    //        Create();

    //        return instance;
    //    }
    //}
    //protected static UIHandler instance;

    //public static UIHandler Create()
    //{
    //    GameObject gameManagerGameObject = new GameObject("GameManager");
    //    instance = gameManagerGameObject.AddComponent<UIHandler>();
    //    instance.enabled = false;
    //    return instance;
    //}

    //private Controls controls;

    //public bool PausingOf { get; private set; }

    //void Start()
    //{
    //    controls = new Controls();
    //    controls.UI.SetCallbacks(this);

    //    controls.UI.Enable();
    //}
    //private void OnDestroy()
    //{//Wyłączyłem to bo wyjebywało mi error nie wiem czemu.
    //    controls.UI.Disable();
    //}

    //public void OnPause(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        PausingOf = true;
    //    }

    //    else if (context.canceled)
    //    {
    //        PausingOf = false;
    //    }

    //}

    //public void OnBackSpace(InputAction.CallbackContext context)
    //{
    //    throw new NotImplementedException();
    //}
}
