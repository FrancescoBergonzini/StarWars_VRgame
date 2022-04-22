using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

    namespace SW_VRGame
    {

        public class SW_RobotCutted : MonoBehaviour
        {
        int sliceCounter = 2;

        //float delayCut = 1f;
            
        XRGrabInteractable _myGrabController;
        MeshCollider myColl;

        void Awake()
        {
            
             myColl = this.gameObject.AddComponent<MeshCollider>();
        }

        private void OnEnable()
        {
            myColl.convex = true;
        }

        private void Start()
        {
            _myGrabController = this.gameObject.AddComponent<XRGrabInteractable>();

            if (this.gameObject != null)
            {
                //StartCoroutine(DelayCut());

                _myGrabController.throwVelocityScale = 3;
                _myGrabController.interactionLayers = InteractionLayerMask.GetMask("Default");
            }
           
        }

        /// <summary>
        /// Rende ritagliabili le mesh
        /// Tolto per ottimizzazione
        /// </summary>
        /// <param name="delayCut"></param>
        /// <returns></returns>
        IEnumerator DelayCut(float delayCut)
        {
            yield return new WaitForSeconds(delayCut);
            //rendile nuovamente tagliabili dopo un secondo
            this.gameObject.tag = Tags.Sliceable;
        }
        }
    }

