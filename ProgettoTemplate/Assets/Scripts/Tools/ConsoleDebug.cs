using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleDebug : Singleton<ConsoleDebug>
{
    [SerializeField]
    protected TextMeshProUGUI textMesh;


   public void Write(string text)
    {
        textMesh.text = text;
    }
}
