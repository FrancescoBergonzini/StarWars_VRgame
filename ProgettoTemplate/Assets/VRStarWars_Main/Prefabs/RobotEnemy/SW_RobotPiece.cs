using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class SW_RobotPiece : MonoBehaviour
    {
        [SerializeField] Rigidbody[] pieces;
        [SerializeField] float timeForDissolve;

        //
        List<MeshCollider> allMeshCall = new List<MeshCollider>();


        private void Start()
        {
            foreach (Rigidbody pezzo in pieces)
            {
                allMeshCall.Add(pezzo.gameObject.GetComponent<MeshCollider>());
            }
        }
        public void DisassemblePieces()
        {
            foreach (Rigidbody pezzo in pieces)
            {
                pezzo.gameObject.transform.parent = null;
                pezzo.useGravity = true;
                pezzo.isKinematic = false;
                //
                Destroy(pezzo.gameObject, 3f + (Random.Range(-1f, 1f)));

            }

            foreach(MeshCollider mesh in allMeshCall)
            {
                mesh.enabled = true;
            }

            
        }

    }
}

