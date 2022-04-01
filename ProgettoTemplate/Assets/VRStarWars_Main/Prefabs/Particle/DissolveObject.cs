using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DissolveObject : MonoBehaviour
{
    [SerializeField]
    private float noiseStrength = 0.25f;

    [SerializeField]
    private float objectHeight = 1.0f;

    [SerializeField]
    private Material dissolveMaterial;


    private void Awake()
    {
        dissolveMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        var time = Time.time * Mathf.PI * 0.25f;

        float height = transform.position.y;
        height += Mathf.Sin(time) * (objectHeight / 2.0f);
        SetHeight(height);
    }

    private void SetHeight(float height)
    {
        dissolveMaterial.SetFloat("_CutoffHeight", height);
        dissolveMaterial.SetFloat("_NoiseStrength", noiseStrength);
    }
}
