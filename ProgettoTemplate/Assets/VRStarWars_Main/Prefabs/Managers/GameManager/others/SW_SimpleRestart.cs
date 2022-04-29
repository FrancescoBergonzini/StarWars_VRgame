using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SW_SimpleRestart : MonoBehaviour
{
    public UnityEvent yourCustomEvent;


    private void OnTriggerEnter(Collider other)
    {
        // Trigger the event!
        yourCustomEvent.Invoke();
    }
}
