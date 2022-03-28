using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(MeshCollider))]
    public class VR_CuttedMesh : MonoBehaviour
    {
        public int sliceCounter = 2;
        public float delayCut = 1f;

        void Awake()
        {
            this.GetComponent<MeshCollider>().convex = true;
        }

        private void Start()
        {
            if (this.gameObject != null)
            {           
                Destroy(this.gameObject, 3);
            }

            StartCoroutine(DelayCut());
        }

        IEnumerator DelayCut()
        {
            yield return new WaitForSeconds(delayCut);
            //rendile nuovamente tagliabili dopo un secondo
            this.gameObject.tag = "Sliceable";
        }
    }
}
