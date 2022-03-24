using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MyUnityEvent : UnityEvent { }

public class Test_EventMaster : MonoBehaviour
{
    //metodo classico diretto
    public delegate void MyEventHandler();
    public event MyEventHandler EventEnabler;
    public bool enable_delegateEvent;

    //metodo action ristretto
    public event Action ActionEvent;
    public bool enable_ActionEvent;

    //metodo UnityEvent
    public MyUnityEvent MyUnityEventHandler = new MyUnityEvent();
    public bool enable_UnityEvent;

    private void Update()
    {
        if (enable_delegateEvent)
        {
            if(EventEnabler != null)
            EventEnabler();
        }

        if (enable_ActionEvent)
        {
            //identico a sopra
            ActionEvent?.Invoke();
        }

        if (enable_UnityEvent)
        {
            MyUnityEventHandler?.Invoke();
        }
    }
}
