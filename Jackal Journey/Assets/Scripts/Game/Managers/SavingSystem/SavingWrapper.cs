using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingWrapper : MonoBehaviour
{
    //const string defaultSaveFile = "save";
    //[SerializeField] float fadeInTime = 0.2f;

    //IEnumerator Start()
    //{
    //    Fader fader = FindObjectOfType<Fader>();
    //    fader.FadeOutImmediate();
    //    yield return GetComponent<JsonSavingSystem>().LoadLastScene(defaultSaveFile);
    //    yield return fader.FadeIn(fadeInTime);
    
    //}


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        Debug.Log("L");
    //        Load();
    //    }

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        Debug.Log("R");
    //        Save();
    //    }
    //}

    //public void Load()
    //{
    //    Debug.Log("Load");
    //    GetComponent<JsonSavingSystem>().Load(defaultSaveFile);
    //}

    //public void Save()
    //{
    //    Debug.Log("Save");
    //    GetComponent<JsonSavingSystem>().Save(defaultSaveFile);
    //}

    //_----------------------------------------------------------------------

    private const string currentSaveKey = "currentSaveName";
    [SerializeField] float fadeInTime = 0.2f;
    [SerializeField] float fadeOutTime = 0.2f;
    [SerializeField] int firstLevelBuildIndex = 1;
    [SerializeField] int menuLevelBuildIndex = 0;

    public void ContinueGame()
    {
        if (!PlayerPrefs.HasKey(currentSaveKey)) return;
        if(!GetComponent<JsonSavingSystem>().SaveFileExists(GetCurrentSave())) return;
        StartCoroutine(LoadLastScene());
    }
   

    public void NewGame(string saveFile)
    {   if (string.IsNullOrEmpty(saveFile)) return;
        SetCurrentSave(saveFile);
        StartCoroutine(LoadFirstScene());
    }

    public void LoadGame(string saveFile)
    {
        SetCurrentSave(saveFile);
        ContinueGame();
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadMenuScene());
    }

    private void SetCurrentSave(string saveFile)
    {
        PlayerPrefs.SetString(currentSaveKey, saveFile);
    }

    private string GetCurrentSave()
    {
        return PlayerPrefs.GetString(currentSaveKey);
    }

    private IEnumerator LoadLastScene()
    {
        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(fadeOutTime);
        yield return GetComponent<JsonSavingSystem>().LoadLastScene(GetCurrentSave());
        yield return fader.FadeIn(fadeInTime);
    }

    private IEnumerator LoadFirstScene()
    {
        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(fadeOutTime);
        yield return SceneManager.LoadSceneAsync(firstLevelBuildIndex);
        yield return fader.FadeIn(fadeInTime);
    }

    private IEnumerator LoadMenuScene()
    {
        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(fadeOutTime);
        yield return SceneManager.LoadSceneAsync(menuLevelBuildIndex);
        yield return fader.FadeIn(fadeInTime);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     Save();
        // }
        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     Load();
        // }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Delete();
        }
    }

    public void Load()
    {
        GetComponent<JsonSavingSystem>().Load(GetCurrentSave());
    }

    public void Save()
    {
        GetComponent<JsonSavingSystem>().Save(GetCurrentSave());
    }

    public void Delete()
    {
        GetComponent<JsonSavingSystem>().Delete(GetCurrentSave());
    }

    public IEnumerable<string> ListSaves()
    {
        return GetComponent<JsonSavingSystem>().ListSaves();
    }
}






