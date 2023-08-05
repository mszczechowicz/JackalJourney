using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject SceneLoadWindow;
    public void Interact()
    {
        SceneLoadWindow.SetActive(true);
    }

}
