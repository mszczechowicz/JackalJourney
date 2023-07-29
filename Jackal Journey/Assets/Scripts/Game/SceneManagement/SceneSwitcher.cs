using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{

    [SerializeField] private int SceneToLoad = 3;

    [SerializeField] private GameObject SceneLoadWindow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SceneLoadWindow == true) { return; }
                
            Debug.Log("PopUpWindow");
            SceneLoadWindow.SetActive(true);


        }

      
    }
    private void OnControllerColliderHit(ControllerColliderHit other)
    {
         if (SceneLoadWindow == true) { return; }
                
            Debug.Log("PopUpWindow");
            SceneLoadWindow.SetActive(true);
    }




}
