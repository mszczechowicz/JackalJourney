using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneLoader
{

    
    public enum Scene {
        MainMenu,
        LoadLevelPlayGround,
        LoadingScene,
        //ADDING THE NAME OF THE SCENE
        Level101,
        Level102,
        Level103

    }

    private static Action onLoaderCallback;

    public static void SceneToLoad(Scene scene)
    {
        //Set the loader callback action to load the target scene
        onLoaderCallback = () =>{SceneManager.LoadScene(scene.ToString());};

        //Load the loading scene
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
       
    }
    public static void SceneToLoad(string scene)
    {
        //Set the loader callback action to load the target scene
        onLoaderCallback = () => { SceneManager.LoadScene(scene.ToString()); };

        //Load the loading scene
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback() { 
    //Triggered after the first Update which lets the screen refresh
    //Execute the loader callback action which will load the target scene
    if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;

        }
    }
    

}
