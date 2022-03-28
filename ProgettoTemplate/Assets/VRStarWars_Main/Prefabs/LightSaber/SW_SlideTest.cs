using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class SW_SlideTest : MonoBehaviour
    {
        [SerializeField] Material mat_cubeSlice;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != "Sliceable")
                return;

            
            if(collision.gameObject.TryGetComponent(out VR_CuttedMesh cutted))
            {
                //se trova già il component significa che è una mesh già tagliata

                if(cutted.sliceCounter > 0)
                {
                    var subsliced = SliceWithCollision.Slice(transform.up, collision, mat_cubeSlice);

                    foreach (var slice in subsliced)
                    {
                        slice.AddComponent<VR_CuttedMesh>().sliceCounter = cutted.sliceCounter - 1;
                    }
                }
                
            }
            else
            {
                //mesh tagliata per la prima volta
                var firstSub = SliceWithCollision.Slice(transform.up, collision, mat_cubeSlice);

                foreach (var slice in firstSub)
                {
                    //crea due nuove mesh tagliabili
                    slice.AddComponent<VR_CuttedMesh>();
                }
            }

        }
    }
}

