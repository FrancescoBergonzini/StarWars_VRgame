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
            //da sistemare
            currentMesh.SetActive(true);
            _myforce = currentMesh.GetComponent<ConstantForce>();
            _rdb = currentMesh.GetComponent<Rigidbody>();
        }

        void ApplyMyLaunchForce(float force)
        {
            _myforce.force = new Vector3(0, 0, force);
            currentForceAppliedInCreate = force;
        }


        #region Pausable
        public bool IsPausable { get; set; }

        public void Pause(bool Pause)
        {
            //li fermo in aria
            if (Pause)
            {
                _myforce.force = new Vector3(0, 0, 0);
            }
            else
            {
                this.ApplyMyLaunchForce(currentForceAppliedInCreate);
            }
        }

       

        #endregion

    }
}

