using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Listener : MonoBehaviour
{
    [SerializeField] Test_EventMaster myMaster;
    private void Start()
    {
        myMaster.EventEnabler += DebugEvent;
        myMaster.ActionEvent += DebugActionEvent;
        myMaster.MyUnityEventHandler.AddListener(DebugUnityEvent);
    }

    private void OnDisable()
    {
        myMaster.EventEnabler -= DebugEvent;
        myMaster.ActionEvent -= DebugActionEvent;
        myMaster.MyUnityEventHandler.RemoveListener(DebugUnityEvent);
    }

    private void DebugEvent()
    {
        Debug.Log("Lanciato evento debug");
    }

    private void DebugActionEvent()
    {
        Debug.Log("Lanciato action debug");
    }

    private void DebugUnityEvent()
    {
        Debug.Log("Lanciato Unityevent debug");
    }
}
