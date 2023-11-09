using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private GameObject playerobject;
   

    private IPlayerData playerData;

    private void Awake()
    {
        playerData = playerobject.GetComponent<IPlayerData>();

    }
    private void Start()
    {
        // LoadData();
        PlayerPrefs.DeleteAll();
    }
    public void SaveData()
    { 
       // string currentScene = SceneManager.GetActiveScene().ToString();
       // PlayerPrefs.SetString("ActiveScene", currentScene);

        Vector3 playerPosition = playerData.GetPosition();
        Debug.Log("Player coordinates, xyz: "+playerPosition);
        PlayerPrefs.SetFloat("PlayerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", playerPosition.z);

        float playerHealth = playerData.GetHealth();
        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);




        PlayerPrefs.Save();
    }
    public void LoadData()
    {
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            float PlayerPositionX = PlayerPrefs.GetFloat("PlayerPositionX");
            float PlayerPositionY = PlayerPrefs.GetFloat("PlayerPositionY");
            float PlayerPositionZ = PlayerPrefs.GetFloat("PlayerPositionZ");
            Vector3 PlayerPosition = new Vector3(PlayerPositionX, PlayerPositionY, PlayerPositionZ);
            Debug.Log("Player coordinates LOADED, xyz: " + PlayerPosition);

            float playerHealth = PlayerPrefs.GetFloat("PlayerHealth");


            playerData.SetHealth(playerHealth);
            playerData.SetPosition(PlayerPosition);

        }
        else
        {
            Debug.Log("EmptySave");
        }
                
    }


   



}
