using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class VR_TextRobotDisabble : MonoBehaviour
    {
        [SerializeField] Rigidbody[] pieces;

        public void DisassemblePieces()
        {
            foreach(Rigidbody pezzo in pieces)
            {
                pezzo.gameObject.transform.parent = null;
                pezzo.useGravity = true;
                pezzo.isKinematic = false;
                pezzo.GetComponent<MeshCollider>().enabled = true; //refactoring
            }
        }
    }
}

