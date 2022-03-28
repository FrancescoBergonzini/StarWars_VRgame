using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

    namespace SW_VRGame
    {
        [RequireComponent(typeof(Rigidbody))]
        [RequireComponent(typeof(MeshCollider))]
        [RequireComponent(typeof(XRGrabInteractable))]
        public class VR_CuttedMesh : MonoBehaviour
        {

        public int sliceCounter = 2;
        public float delayCut = 1f;
            
        XRGrabInteractable _myGrabController;
        InteractionLayerMask layerToGrab = InteractionLayerMask.GetMask("Grab");

        void Awake()
        {
            this.GetComponent<MeshCollider>().convex = true;
            _myGrabController = GetComponent<XRGrabInteractable>();

            _myGrabController.throwVelocityScale = 4;
            _myGrabController.interactionLayers = layerToGrab;
        }

        private void Start()
        {
            if (this.gameObject != null)
            {
                //modifica
                Destroy(this.gameObject, 5);
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

