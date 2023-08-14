using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject SceneLoadWindow;
    public void Interact()
    {
        SceneManager.LoadScene("Gym");
        //SceneLoadWindow.SetActive(true);
    }

}
