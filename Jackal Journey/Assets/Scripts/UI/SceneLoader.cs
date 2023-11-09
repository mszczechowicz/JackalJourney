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
           // Debug.Log("Rozpoczêcie ³adowania sceny: " + scene);
            GameObject loadingGameObject = new GameObject("LoadingGameObject");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(Transition(scene));
           

        };

        //Load the loading scene
        if (scene != Scene.LoadingScene.ToString())
        {
            // Za³aduj scenê "LoadingScene"
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
        }
    }


    public static IEnumerator Transition(string scene)
    {


        //Rozpocznij ³adowanie sceny asynchronicznie
        Debug.Log("Rozpoczêcie ³adowania sceny: " + scene);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.ToString());

        //Oczekuj na zakoñczenie ³adowania
        while (!asyncLoad.isDone)
        {
            yield return null;
        }



        //yield return new WaitUntil(() => onLoaderCallback == null);
        Debug.Log("Zakoñczenie ³adowania sceny: " + scene);
    }



    //public static void LoaderCallback() { 
    ////Triggered after the first Update which lets the screen refresh
    ////Execute the loader callback action which will load the target scene
    //if (onLoaderCallback != null)
    //    {
    //        onLoaderCallback();
    //        onLoaderCallback = null;
    //        Debug.Log("Zakoñczenie ³adowania sceny: LoadingScene");
    //    }
    //}
    
    //KOD ODPOWIADAJ¥CY ZA WCZYTYWANIE LEVELU ZA POMOC¥ GOTOWYCH ENUMÓW
    // public static void SceneToLoad(Scene scene)
    //{
    //    //Set the loader callback action to load the target scene
    //    onLoaderCallback = () => {
    //        Debug.Log("Rozpoczêcie ³adowania sceny: " + scene.ToString());
    //        SceneManager.LoadSceneAsync(scene.ToString());
    //    };

    //    //Load the loading scene
    //    SceneManager.LoadScene(Scene.LoadingScene.ToString());
    //    Debug.Log("Zakoñczenie ³adowania sceny LoadingScene");

    //}
}
