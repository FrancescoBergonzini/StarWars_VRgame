using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SW_VRGame { }

public class SW_Tutorial : MonoBehaviour
{
    int currentAdvice = 0;

    [Header("Direct reference")]
    [SerializeField] Image Advice_Image;
    [SerializeField] TextMeshProUGUI Advice_text;
    [SerializeField] TextMeshProUGUI Title_text;
    [SerializeField] int maxNumberAdvice = 4;

    [Header("Tutorial Text")]
    [TextArea(3, 10)]
    [SerializeField] string[] adviceText;
    [SerializeField] Sprite[] allImages;
    [SerializeField] string[] allTitle;
    

    public void ChangeAdvice(bool change) //passa true per aumentare, false per diminuire
    {
        if (change)
            currentAdvice++;
        else 
            currentAdvice--;

        if (currentAdvice > maxNumberAdvice-1)
            currentAdvice = 0;

        if(currentAdvice < 0)
            currentAdvice = maxNumberAdvice-1;

        //cambia immagine
        //cambia titolo e advice

        Advice_Image.sprite = allImages[currentAdvice];
        Advice_text.text = adviceText[currentAdvice];
        Title_text.text = allTitle[currentAdvice];
    }
    
}
