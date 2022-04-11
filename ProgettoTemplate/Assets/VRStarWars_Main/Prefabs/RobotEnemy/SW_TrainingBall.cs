using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SW_VRGame
{

    public class SW_TrainingBall : Item, IPausable
    {
        ConstantForce _myforce;
        Rigidbody _rdb;
        float currentForceAppliedInCreate;

        //
        [SerializeField] GameObject[] typeOfMesh;
        [HideInInspector] public Transform robot_active;

        public static SW_TrainingBall Create(SW_TrainingBall prefab, Vector3 Tposition, float force, Transform parent)
        {
            SW_TrainingBall ball = Instantiate(prefab, Tposition, Quaternion.identity, parent);

            ball.ChooseRandomMesh(); //da sistemare
            ball.ApplyMyLaunchForce(force);

            return ball;
        }


        //metodi gestione enemy
        public void ChooseRandomMesh()
        {
            //choose one mesh
            var currentMesh = typeOfMesh[Random.Range(0, typeOfMesh.Length)];
            robot_active = currentMesh.transform;

            //da sistemare
            currentMesh.SetActive(true);
            _myforce = currentMesh.GetComponent<ConstantForce>();
            _rdb = currentMesh.GetComponent<Rigidbody>();
        }

        void ApplyMyLaunchForce(float force)
        {
            if(_myforce != null)
            {
                _myforce.force = new Vector3(0, 0, force);
                currentForceAppliedInCreate = force;
            }
            
        }


        private void Start()
        {
            IsPausable = true;
        }

        #region Pausable
        public bool IsPausable { get; set; }

        public void Pause(bool Pause)
        {
            if (this.gameObject == null)
                return;

            Debug.Log("cHIAMATO PAUSE" + Pause + "DA" + gameObject.name);
            
            //li fermo in aria
            if (Pause)
            {
                _rdb.isKinematic = true;
                
            }
            else
            {
                _rdb.isKinematic = false;
            }
            
        }

       

        #endregion

    }
}

