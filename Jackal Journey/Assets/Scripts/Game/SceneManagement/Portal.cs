using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{

   
    [SerializeField] private string SceneName;

    
    public void Interact()
    {
        SceneManager.LoadScene(SceneName);
        //SceneLoader.SceneToLoad(SceneName);
    }

}
