using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class SW_Canestro : Singleton<SW_Canestro>
    {
        [SerializeField] TemplateGameManager currentScene_GameManager;
        [SerializeField] GameObject robotdestroy_particle;

        //genera evento update score al canestro del pezzo nemico
        public Event_Score Event_UpdateScoreCanestro = new Event_Score();

        protected override void OnAwake()
        {         
            if (currentScene_GameManager == null)
            {
                currentScene_GameManager = FindObjectOfType<TemplateGameManager>();
            }

            base.OnAwake();
            
        }


        private void OnTriggerEnter(Collider other)
        {
            Debug.LogWarning("Collisione con" + other.name);

            //particle
            Destroy(Instantiate(robotdestroy_particle, other.transform.position, Quaternion.identity),3);
            //aggiorna punteggio
            Event_UpdateScoreCanestro.Invoke(3);

            Destroy(other.gameObject);

            

        }

    }
}

