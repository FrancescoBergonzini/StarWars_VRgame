using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SW_VRGame
{

    public class VR_TrainingBall : Item, IPausable
    {
        ConstantForce _myforce;
        Rigidbody _rdb;

        //
        [SerializeField] GameObject[] typeOfMesh;

        public static VR_TrainingBall Create(VR_TrainingBall prefab, Vector3 Tposition, float force)
        {
            VR_TrainingBall ball = Instantiate(prefab, Tposition, Quaternion.identity);

            ball.ChooseRandomMesh(); //da sistemare
            ball.ApplyMyLaunchForce(force);
            return ball;
        }


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
        }


        #region Pausable
        public bool IsPausable => throw new System.NotImplementedException();

        public void Pause(bool Pause)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

