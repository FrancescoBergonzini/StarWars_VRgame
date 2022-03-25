using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Taglio : MonoBehaviour
{
    //Materiale delle parti tagliate
    public Material materialAfterSlice;

    //Aggiungo un layer per lo stato di taglio
    public LayerMask sliceMask;

    //Verifico se avviene taglio
    public bool isCut;

    // Update is called once per frame
    void Update()
    {
        //Quando avviene il taglio
        if (isCut == true)
        {
            isCut = false; //cambio stato

            //Creo un array di oggetti tagliati
            Collider[] objectsToSlice = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);

            //Ad ogni oggetti nell'array richiamo la funzione di Ezyslice
            foreach (Collider objectToSlice in objectsToSlice)
            {
                SlicedHull slicedObject = SliceObject(objectToSlice.gameObject, materialAfterSlice);

                //Crea una parte superiore dal taglio
                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToSlice.gameObject, materialAfterSlice);
                //Crea una parte inferiore dal taglio
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToSlice.gameObject, materialAfterSlice);

                //Ritorna posizione parte tagliata superiore
                upperHullGameobject.transform.position = objectToSlice.transform.position;
                //Ritorna posizione parte tagliata inferiore
                lowerHullGameobject.transform.position = objectToSlice.transform.position;

                ///Richiamo metodo a entrambi le parti per la fisica
                MakeItPhysical(upperHullGameobject);
                MakeItPhysical(lowerHullGameobject);

                Destroy(objectToSlice.gameObject);
            }


        }
    }
    //metodo che gestisce la fisica degli oggettai tagliati
    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();     //.AddForce(obj.transform.position, ForceMode.Impulse);
        
    }

    //metodo privato che richiamo in update che mi restituisce oggetto tagliato e materiale
    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }

}
