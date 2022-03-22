using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class SW_LightSaber : Singleton<SW_LightSaber>, IPausable
    {
        public static bool isBeenGrabbed;

        [SerializeField] Collider swordCollider;

        //laser della spada
        [SerializeField] GameObject laser;

        //dimensione della spada con laser attivo
        private Vector3 fullSize;

        public static bool isActive; //false

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
            //Accendo il laser se il transform del local scale di y è minore del fullsize di y
            if (isActive && laser.transform.localScale.y < fullSize.y)
            {
                laser.transform.localScale += new Vector3(0, 0.0001f, 0);
                isActive = true;
                swordCollider.enabled = true;
            }
            else
            {
                if (!isActive && laser.transform.localScale.y >= 0.0001)
                {
                    laser.transform.localScale += new Vector3(0, -0.0001f, 0);
                    isActive = false;
                    swordCollider.enabled = false;
                }
            }

        }
        public void SwordGrabbed(bool grabbed)
        {
            Debug.Log("Stato spada grabbata: " + grabbed);
            isBeenGrabbed = grabbed;
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

