using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceWithCollision 
{
    public static GameObject[] Slice(Vector3 longitudeDirection , Collision collision, Material crossMaterial)
    {
        GameObject[] gameObjects = new GameObject[2];
        ContactPoint firstContactPoint = collision.contacts[0];
        Vector3 slicePlaneNormal = Vector3.Cross(longitudeDirection, firstContactPoint.normal);
        if (slicePlaneNormal == Vector3.zero) slicePlaneNormal = new Vector3(1f, 0, 0);
        var sliceHull = collision.gameObject.Slice(firstContactPoint.point, slicePlaneNormal, crossMaterial);
        gameObjects[0] = sliceHull.CreateLowerHull(collision.collider.gameObject, crossMaterial);
        gameObjects[1] = sliceHull.CreateUpperHull(collision.collider.gameObject, crossMaterial);
        GameObject.Destroy(collision.collider.gameObject);
        return gameObjects;
    }


    public static GameObject[] Slice(Transform transform,  Vector3 longitudeDirection, Collider collider , Vector3 direction, Material crossMaterial)
    {
        GameObject[] gameObjects = new GameObject[2];
        Vector3 firstContactPoint = collider.ClosestPoint(transform.position);
        Vector3 slicePlaneNormal = Vector3.Cross(longitudeDirection, direction);
        var sliceHull = collider.gameObject.Slice(firstContactPoint, slicePlaneNormal, crossMaterial);
        gameObjects[0] = sliceHull.CreateLowerHull(collider.gameObject, crossMaterial);
        gameObjects[1] = sliceHull.CreateUpperHull(collider.gameObject, crossMaterial);
        GameObject.Destroy(collider.gameObject);
        return gameObjects;
    }
}
