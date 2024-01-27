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
    [SerializeField] public GameObject quotePrefab;

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

        GameObject quoteInstance = Instantiate(quotePrefab, canvasObject.transform);

        TMP_Text quote = quoteInstance.GetComponentInChildren<TMP_Text>();
        quote.text = dialogText;
        Destroy(canvasObject, dialogDuration);
    }

}
