using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{

   
    [SerializeField] private string SceneName;
    //private Dictionary<string, string> Save;

    //public void SaveIntoJson()
    //{
    //    Save = new Dictionary<string, string>() {
    //    {
    //        "health", Player.health
    //    },
    //    {
    //        "positionX", "100"
    //    },
    //    {
    //        "positionY", "100"
    //    }
    //};  
    //    PlayerPrefs.SetString("health", Player.health)
    //    string potion = JsonUtility.ToJson(Save);
    //    System.IO.File.WriteAllText(Application.persistentDataPath + "/PotionData.json", potion);
    //}
    public void Interact()
    {
        DontDestroyOnLoad(gameObject);
        //SceneManager.LoadScene(SceneName);
        SceneLoader.SceneToLoad(SceneName);
        
    }

}
