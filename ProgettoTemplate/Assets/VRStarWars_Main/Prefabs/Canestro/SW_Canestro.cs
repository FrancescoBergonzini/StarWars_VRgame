using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class SW_Canestro : Singleton<SW_Canestro>
    {
        [SerializeField] TemplateGameManager currentScene_GameManager;

        protected override void OnAwake()
        {         
            if (currentScene_GameManager == null)
            {
                currentScene_GameManager = FindObjectOfType<TemplateGameManager>();
            }

            base.OnAwake();
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            //
            Debug.Log("Collisione di " + collision.gameObject.name);

            //check ottimizzato => distruggi tutto, ma assegna punto solo per Oggetto giusto

            collision.collider.enabled = false;
            Destroy(collision.gameObject);

            SW_CutBladeLogic.Instance.UpdatescoreEnabler.AddListener(UpdateScore);
        }

        //aggiorno punteggio
        void UpdateScore(int value)
        {
            currentScene_GameManager.gameScore += value;
        }
    }
}

