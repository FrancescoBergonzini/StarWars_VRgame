using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class Canestro : MonoBehaviour
    {
        TemplateGameManager currentScene_GameManager;

        private void Awake()
        {
            
            if (currentScene_GameManager == null)
            {
                currentScene_GameManager = FindObjectOfType<TemplateGameManager>();
            }
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            //
            Debug.Log("Collisione di " + collision.gameObject.name);
            Destroy(collision.gameObject);

            SW_CutBladeLogic.Instance.UpdatescoreEnabler.AddListener(UpdateScore);
        }

        void UpdateScore(int value)
        {
            currentScene_GameManager.gameScore += value;
        }
    }
}

