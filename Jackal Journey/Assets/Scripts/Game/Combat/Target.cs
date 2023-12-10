using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{   
    public event Action<Target> OnDestroyed; 

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
