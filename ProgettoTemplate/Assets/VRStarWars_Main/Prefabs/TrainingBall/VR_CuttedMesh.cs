using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

    namespace SW_VRGame
    {
        [RequireComponent(typeof(Rigidbody))]
        [RequireComponent(typeof(MeshCollider))]
        public class VR_CuttedMesh : MonoBehaviour
        {

        public int sliceCounter = 2;
        public float delayCut = 1f;
            
        XRGrabInteractable _myGrabController;

        void Awake()
        {
            this.GetComponent<MeshCollider>().convex = true;
            _myGrabController = GetComponent<XRGrabInteractable>();

            _myGrabController = this.gameObject.AddComponent<XRGrabInteractable>();
           
        }

        private void Start()
        {
            if (this.gameObject != null)
            {
                //modifica
                Destroy(this.gameObject, 5);

                StartCoroutine(DelayCut());

                _myGrabController.throwVelocityScale = 4;
                _myGrabController.interactionLayers = InteractionLayerMask.GetMask("Default");
            }
           
        }

        IEnumerator DelayCut()
        {
            yield return new WaitForSeconds(delayCut);
            //rendile nuovamente tagliabili dopo un secondo
            this.gameObject.tag = "Sliceable";
        }
        }
    }

