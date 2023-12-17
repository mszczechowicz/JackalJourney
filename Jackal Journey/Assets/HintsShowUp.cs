using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HintsShowUp : MonoBehaviour
{
    [SerializeField] GameObject HintsImage;

    private const string HintsShowed = " HintsShowed";
    

    private void Start()
    {
        bool ShowNextTime = PlayerPrefs.GetInt(HintsShowed, 0) == 1;

        if (ShowNextTime)
        {
            HintsImage.SetActive(false);

        }
        else
        {
            StartCoroutine(CloseHint());
        }
    }

    private IEnumerator CloseHint()
    {
        yield return new WaitForSeconds(5);
        HintsImage?.SetActive(false);
        PlayerPrefs.SetInt(HintsShowed, 1);

    }
}