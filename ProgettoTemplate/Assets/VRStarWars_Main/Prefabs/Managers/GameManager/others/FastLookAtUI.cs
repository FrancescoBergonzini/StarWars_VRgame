using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastLookAtUI : MonoBehaviour
{
    Camera camera_main;
    private void Start()
    {
        camera_main = Camera.main;
    }
    

    private void Update()
    {
        transform.LookAt(transform.position + (camera_main.transform.rotation * Vector3.back), camera_main.transform.rotation * Vector3.down);
    }
    
}
