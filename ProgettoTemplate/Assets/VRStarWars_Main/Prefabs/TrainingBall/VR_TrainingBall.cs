using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class VR_TrainingBall : Item, IPausable
    {
        [SerializeField] ConstantForce _myforce;
        [SerializeField] Rigidbody _rdb;


        public static VR_TrainingBall Create(VR_TrainingBall prefab, Vector3 Tposition, float force)
        {
            VR_TrainingBall ball = Instantiate(prefab, Tposition, Quaternion.identity);
            ball.ApplyMyLaunchForce(force);
            return ball;
        }

        private void OnEnable()
        {
            //
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Sword")
            {
                //test
                _myforce.enabled = false;
                _rdb.useGravity = true;

                Destroy(this.gameObject, 4);
            }
            
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

