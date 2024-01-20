using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{   
    public event Action<Target> OnDestroyed;
    [SerializeField] public GameObject TargetImage;
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
