using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    /// <summary>
    /// Si occupa di far iniziare la newWave, si attiva e disattiva
    /// </summary>
    /// 
    
    public class SW_Start_Cube : MonoBehaviour
    {
        public System.Action startnewWave;
        public GameObject particle;

        bool activeMyself;
        public bool ActiveMyself
        {
            get { return activeMyself; }
            set
            {
                if(value && activeMyself != value)
                {
                    this.gameObject.SetActive(true);
                    activeMyself = value;
                }

                if(!value && activeMyself != value)
                {
                    this.gameObject.SetActive(false);
                    activeMyself = value;
                }
            }
        }

        private void Start()
        {
            ActiveMyself = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            startnewWave();
            Destroy(Instantiate(particle), 5);
        }
    }
}

