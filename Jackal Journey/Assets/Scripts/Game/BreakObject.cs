using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BreakObject : MonoBehaviour
{
    public GameObject fractured;
    public GameObject savefractured;
    public TMP_FontAsset customFont;
    public int myLayer;
    public UnityEvent onDestroy_UnityEvent;



    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.gameObject.layer == myLayer)
        {
            CreatePopUpObtainedItem();
            BreakTheThing();
        }
    }



    public void BreakTheThing()
    {
        
        Debug.Log("breakthething");
        savefractured = Instantiate(fractured, transform.position, transform.rotation);
        savefractured.transform.localScale = gameObject.transform.localScale; // change its local scale to match object
        savefractured.GetComponentInChildren<AudioSource>().Play();
        Debug.Log("makeinstant");
        Destroy(gameObject);    
        Destroy(savefractured, 3f);
        
    }

    void CreatePopUpObtainedItem()
    {
        GameObject canvasObject = new GameObject("Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        GameObject textObject = new GameObject("Text");
        textObject.transform.SetParent(canvasObject.transform, false);
        TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
        textComponent.text = "Soft Sand Obtained";
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            rectTransform = textObject.AddComponent<RectTransform>();
        }

        // Ustaw pozycjê na lewym górnym rogu
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);

        // Dostosuj dodatkowe w³aœciwoœci, jeœli to konieczne
        rectTransform.anchoredPosition = new Vector2(10, -10); // Ustawienie odstêpu od lewego górnego rogu
        textComponent.fontSize = 24;
        textComponent.color = Color.white;
        // Ustaw pozycjê w 2/5 wysokoœci od do³u
        rectTransform.anchoredPosition = new Vector2(10, -Screen.height * 2 / 5f);


       
        // Ustaw szerokoœæ Canvasu
        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080); // Dostosuj do rozmiarów Twojego ekranu



        textComponent.font = customFont;
        textComponent.fontSize = 32;
        textComponent.color = Color.white;
        Destroy(canvasObject, 2f);

    }
}
