using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioItem = SW_VRGame.AudioManager.AudioType;

namespace SW_VRGame
{
    /// <summary>
    /// Logica spada base, gestisce attivazione e pausa, non il cut
    /// </summary>
    public class SW_LightSaber : Singleton<SW_LightSaber>
    {
        [HideInInspector]
        public bool isBeenGrabbed;
        [HideInInspector]
        public bool isPaused; //false

        [SerializeField] Collider swordCollider;
        [SerializeField] GameObject laser;

        public bool accendo = false;

        //dimensione della spada con laser attivo
        private Vector3 fullSize;

        // Start is called before the first frame update
        void Start()
        {
            //trasformiamo la scala locale per salvare qualunque sia la dimensione intera
            fullSize = laser.transform.localScale;

            //al di là della scala creiamo un nuovo vettore che mette a  0 la y
            laser.transform.localScale = new Vector3(fullSize.x, 0, fullSize.z);
        }

        // Update is called once per frame
        void Update()
        {
            //Accendo il laser se il transform del local scale di y è minore del fullsize di y
            if (isPaused  && laser.transform.localScale.y < fullSize.y)
            {
                //ATTIVO
                laser.transform.localScale += new Vector3(0, 0.0001f, 0);
                isPaused = true;
               
                swordCollider.enabled = true;
            }
            else
            {
                //SPENTO
                if (!isPaused && laser.transform.localScale.y >= 0.0001)
                {
                    laser.transform.localScale += new Vector3(0, -0.0001f, 0);
                    isPaused = false;
                    swordCollider.enabled = false;


                }
            }
           /*if(accendo && laser.transform.localScale.y < fullSize.y)
            {
                //ATTIVO
                laser.transform.localScale += new Vector3(0, 0.0001f, 0);
            }

            if (!accendo && laser.transform.localScale.y >= 0.0001)
            {
                //NON ATTIVO
                laser.transform.localScale += new Vector3(0, -0.0001f, 0);
            }*/

        }


        //chiamato dall XR Grab Interactable
        public void SwordGrabbed(bool grabbed)
        {
            isBeenGrabbed = grabbed;

        }

    }
}

