using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] public string dialogText = "WprowadŸ swój tekst tutaj";
    [SerializeField] public Sprite dialogImage;
    [SerializeField] public float dialogDuration = 2f;
   

    private TextMeshProUGUI dialogTextMesh;
    private Image dialogImageField;
    private Canvas canvas;

    public void ShowDialog()
    {   
        CreateDialogue();
        
    }


    void CreateDialogue()
    {
        GameObject canvasObject = new GameObject("Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;


        // Utwórz obiekt TextMeshPro dla tekstu
        GameObject textObject = new GameObject("DialogText");
        dialogTextMesh = textObject.AddComponent<TextMeshProUGUI>();
        dialogTextMesh.transform.SetParent(canvas.transform);
        dialogTextMesh.text = dialogText;        

        // Utwórz obiekt Image dla obrazu
        GameObject imageObject = new GameObject("DialogImage");
        dialogImageField = imageObject.AddComponent<Image>();
        dialogImageField.transform.SetParent(canvas.transform);
        dialogImageField.sprite = dialogImage;

        // Ustaw obiekt Image jako child obiektu Text, aby zachowaæ jednolit¹ lokalizacjê
        imageObject.transform.SetParent(textObject.transform);

        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            rectTransform = textObject.AddComponent<RectTransform>();
        }

        // Ustaw anchorMin, anchorMax i pivot dla œrodka dolnego krawêdzi ekranu
        rectTransform.anchorMin = new Vector2(0.5f, 0);
        rectTransform.anchorMax = new Vector2(0.5f, 0);
        rectTransform.pivot = new Vector2(0.5f, 0);

        // Ustaw odleg³oœæ od do³u ekranu
        float screenHeight = Screen.height;
        float offsetFromBottom = 200f;
        rectTransform.anchoredPosition = new Vector2(0, offsetFromBottom);

        // Ustaw szerokoœæ i wysokoœæ obiektu  image oraz textu
        dialogTextMesh.GetComponent<RectTransform>().sizeDelta = new Vector2(700f, rectTransform.sizeDelta.y);
        dialogTextMesh.GetComponent<RectTransform>().sizeDelta = new Vector2(300f, rectTransform.sizeDelta.x);
        dialogImageField.GetComponent<RectTransform>().sizeDelta = new Vector2(700f, rectTransform.sizeDelta.y);
        dialogImageField.GetComponent<RectTransform>().sizeDelta = new Vector2(300f, rectTransform.sizeDelta.x);

        dialogTextMesh.text = dialogText;
        dialogImageField.sprite = dialogImage;
        Destroy(canvasObject, 3f);
    }

}
