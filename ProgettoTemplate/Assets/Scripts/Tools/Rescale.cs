using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndRescaleEvent : UnityEvent<Rescale> { }

/// <summary>
/// Classe per fare lo scale animato di un gameobject
/// </summary>
public class Rescale : MonoBehaviour, IPausable
{
    public EndRescaleEvent EndRescaleEvent { get; protected set; } = new EndRescaleEvent();
    bool pause = false;

    public bool IsPausable { get; set; }

    /// <summary>
    /// Run dell'animazione di rescale
    /// </summary>
    /// <param name="startScale"></param>
    /// <param name="finalScale"></param>
    /// <param name="scaleInTime"></param>
    public void StartScale(Vector3 startScale, Vector3 finalScale, float scaleInTime)
    {
        StartCoroutine(ScaleAnimation(startScale, finalScale, scaleInTime));
    }

    public void StopRescale()
    {
        StopAllCoroutines();
    }


    /// <summary>
    /// Mette l'animazione in pausa/resume
    /// </summary>
    /// <param name="Pause"></param>
    public void Pause(bool Pause)
    {
        pause = Pause;
    }


    protected IEnumerator ScaleAnimation(Vector3 startScale, Vector3 finalScale, float scaleInTime)
    {
        float currTime = 0;
        transform.localScale = startScale;

        while (currTime < scaleInTime)
        {
            if (!pause)
            {
                currTime += Time.deltaTime;
                float normalizedTime = currTime / scaleInTime;
                var currScale = Vector3.Lerp(startScale, finalScale, normalizedTime);
                transform.localScale = currScale;
            }
            yield return null;
        }
        transform.localScale = finalScale;
        EndRescaleEvent.Invoke(this);
    }
}
