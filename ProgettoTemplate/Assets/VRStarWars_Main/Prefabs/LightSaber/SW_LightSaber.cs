using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    /// <summary>
    /// Logica spada base, gestisce attivazione e pausa
    /// </summary>
    public class SW_LightSaber : Singleton<SW_LightSaber>
    {
        public bool isBeenGrabbed;
        public bool isPaused; //false

        [SerializeField] Collider swordCollider;
        [SerializeField] GameObject laser;

        //dimensione della spada con laser attivo
        private Vector3 fullSize;

        // Start is called before the first frame update
        void Start()
        {
            //trasformiamo la scala locale per salvare qualunque sia la dimensione intera
            fullSize = laser.transform.localScale;

            //al di là della scala creiamo un nuovo vettore che mette a  0 la y
            laser.transform.localScale = new Vector3(fullSize.x, 0.0001f, fullSize.z);
        }

        // Update is called once per frame
        void Update()
        {
            //da sistemare REFACTORING
            //Accendo il laser se il transform del local scale di y è minore del fullsize di y
            if (isPaused && laser.transform.localScale.y < fullSize.y)
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


        }

        //chiamato dall XR Grab Interactable
        public void SwordGrabbed(bool grabbed)
        {
            isBeenGrabbed = grabbed;

        }

    }
}

