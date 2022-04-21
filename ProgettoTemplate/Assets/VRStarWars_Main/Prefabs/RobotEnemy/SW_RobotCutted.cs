using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

    namespace SW_VRGame
    {
        [RequireComponent(typeof(Rigidbody))]
        [RequireComponent(typeof(MeshCollider))]
        public class SW_RobotCutted : MonoBehaviour
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
                StartCoroutine(DelayCut());

                _myGrabController.throwVelocityScale = 2;
                _myGrabController.interactionLayers = InteractionLayerMask.GetMask("Default");
            }
           
        }

        IEnumerator DelayCut()
        {
            yield return new WaitForSeconds(delayCut);
            //rendile nuovamente tagliabili dopo un secondo
            this.gameObject.tag = Tags.Sliceable;
        }
        }
    }

