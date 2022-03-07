using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Component per fare in modo che una UI canvas segua lo sguardo del giocatore (anche con un ritardo damp)
/// </summary>
public class UICanvasFollower : MonoBehaviour
{
    [SerializeField]
    protected float distance;

    [SerializeField]
    protected float damp = 1;


    void Update()
    {
        Vector3 newPos = Camera.main.transform.position + Camera.main.transform.forward * distance;
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, damp);
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
