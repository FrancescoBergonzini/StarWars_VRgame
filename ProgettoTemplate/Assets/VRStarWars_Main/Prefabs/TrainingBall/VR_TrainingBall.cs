using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace SW_VRGame
{
    //evento Unity, publisher
    public class Event_Score : UnityEvent<int> { }

    public class VR_TrainingBall : Item, IPausable
    {
        [SerializeField] ConstantForce _myforce;
        [SerializeField] Rigidbody _rdb;
        bool InGame = true;

        //evento con tipo il delegate indicato sopra
        public Event_Score UpdatescoreEnabler = new Event_Score();

        public static VR_TrainingBall Create(VR_TrainingBall prefab, Vector3 Tposition, float force)
        {
            VR_TrainingBall ball = Instantiate(prefab, Tposition, Quaternion.identity);
            //ball.ApplyMyLaunchForce(force);
            return ball;
        }


        //
        /*
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Sword" && InGame)
            {
                InGame = !InGame;

                _myforce.enabled = false;
                _rdb.useGravity = true;

                //quando distrutto chiamo evento score
                UpdatescoreEnabler.Invoke(1); //passo 1, come parametro
                
                Destroy(this.gameObject, 3);

                //notifica lo score dell'avvenuta distruzione
            }
            
        }
        */

        private void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2);
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

