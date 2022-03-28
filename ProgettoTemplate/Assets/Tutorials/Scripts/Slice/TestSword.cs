using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSword : MonoBehaviour
{
    public Transform target;
    public Material crossMaterial = null;
    public float force = 1;
    Vector3 director;
    Rigidbody rb;

    private void Start()
    {
        director = target.position - transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(director.normalized * force, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider collider)
    {

        GameObject[] gameObjects = SliceWithCollision.Slice(transform, transform.up, collider, rb.velocity.normalized, null);
        Debug.Log("Trigger!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject[] gameObjects = SliceWithCollision.Slice(transform.up, collision, null);
        Debug.Log("Collision!");
    }
}
