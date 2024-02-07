using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class LoadFirstScene : MonoBehaviour
{
    private void OnEnable()
    {
       SceneManager.LoadScene(2);
    }
}
