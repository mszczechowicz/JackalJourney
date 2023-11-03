using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate) { 
        isFirstUpdate = false;
            SceneLoader.LoaderCallback();
            Debug.Log("Scene has been Loaded");
        
        
        }

    }
}
