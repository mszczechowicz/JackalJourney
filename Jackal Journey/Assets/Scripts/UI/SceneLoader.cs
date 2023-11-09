using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneLoader
{
    private class LoadingMonoBehaviour : MonoBehaviour { }
    
    public enum Scene {
        MainMenu,
        LoadingScene,
    }

    private static Action onLoaderCallback;

   


    //Loading scene by string name
    public static void Load(string scene)
    {
        //Set the loader callback action to load the target scene
        onLoaderCallback = () => {
           // Debug.Log("Rozpoczęcie ładowania sceny: " + scene);
            GameObject loadingGameObject = new GameObject("LoadingGameObject");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(Transition(scene));
           

        };

        //Load the loading scene
        if (scene != Scene.LoadingScene.ToString())
        {
            // Załaduj scenę "LoadingScene"
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
        }
    }


    public static IEnumerator Transition(string scene)
    {


        //Rozpocznij ładowanie sceny asynchronicznie
        Debug.Log("Rozpoczęcie ładowania sceny: " + scene);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.ToString());

        //Oczekuj na zakończenie ładowania
        while (!asyncLoad.isDone)
        {
            yield return null;
        }



        //yield return new WaitUntil(() => onLoaderCallback == null);
        Debug.Log("Zakończenie ładowania sceny: " + scene);
    }



    //public static void LoaderCallback() { 
    ////Triggered after the first Update which lets the screen refresh
    ////Execute the loader callback action which will load the target scene
    //if (onLoaderCallback != null)
    //    {
    //        onLoaderCallback();
    //        onLoaderCallback = null;
    //        Debug.Log("Zakończenie ładowania sceny: LoadingScene");
    //    }
    //}
    
    //KOD ODPOWIADAJĄCY ZA WCZYTYWANIE LEVELU ZA POMOCĄ GOTOWYCH ENUMÓW
    // public static void SceneToLoad(Scene scene)
    //{
    //    //Set the loader callback action to load the target scene
    //    onLoaderCallback = () => {
    //        Debug.Log("Rozpoczęcie ładowania sceny: " + scene.ToString());
    //        SceneManager.LoadSceneAsync(scene.ToString());
    //    };

    //    //Load the loading scene
    //    SceneManager.LoadScene(Scene.LoadingScene.ToString());
    //    Debug.Log("Zakończenie ładowania sceny LoadingScene");

    //}
}
