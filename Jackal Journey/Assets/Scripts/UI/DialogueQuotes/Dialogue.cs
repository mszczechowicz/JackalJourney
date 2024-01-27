using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [TextAreaAttribute]
    [SerializeField] public string dialogText = "Wprowadü swÛj tekst tutaj";
    
    [SerializeField] public Sprite dialogImage;
    [SerializeField] public float dialogDuration = 2f;
    [SerializeField] public GameObject quotePrefab;

    [SerializeField] public string characterName = "Wprowadü nazwe";
    [SerializeField] public Color characternamedisplaycolor;
    public void ShowDialog()
    {   
        CreateDialogue();        
    }


    void CreateDialogue()
    {      
        GameObject canvasObject = new GameObject("Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GameObject quoteInstance = Instantiate(quotePrefab, canvasObject.transform);

        TMP_Text[] textComponents = quoteInstance.GetComponentsInChildren<TMP_Text>();

        // Sprawdü, czy istnieje co najmniej jeden komponent
        if (textComponents.Length > 0)
        {
            // Zmiana pierwszego komponentu TMP_Text
            TMP_Text name = textComponents[0];
            name.text = characterName;

            name.color = Color.blue;

            // Sprawdü, czy istnieje drugi komponent
            if (textComponents.Length > 1)
            {
                // Zmiana drugiego komponentu TMP_Text
                TMP_Text quote = textComponents[1];
                quote.text = dialogText;
                
            }
        }

        Destroy(canvasObject, dialogDuration);
    }

}
