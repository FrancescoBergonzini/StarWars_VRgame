using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class SW_SpawnManager : Singleton<SW_SpawnManager>
    {
        [SerializeField] SW_Spawner[] allSpawner;
        [SerializeField] float launchforce;

        [SerializeField] float delay;
        [SerializeField] VR_TrainingBall prefab_ball;
        //

        [SerializeField] TemplateGameManager currentScene_GameManager;

        protected override void OnAwake()
        {
            //
            if (currentScene_GameManager == null)
            {
                currentScene_GameManager = FindObjectOfType<TemplateGameManager>();
            }

            base.OnAwake();
        }

        private void Start()
        {
            //mi iscrivo all'evento spada per sapere quando aggiornare lo score
            SW_CutBladeLogic.Instance.UpdatescoreEnabler.AddListener(UpdateScore);

            Test_SpawnBasicRoutine(delay);
        }

        void Test_SpawnBasicRoutine(float delay)
        {

            IEnumerator spawnBasicRoutine(float delay)
            {
                while (true)
                {
                    Debug.Log("Spawn una mesh");
                    var variaDelay = Random.Range(delay - 1f, delay + 1f);
                    //choose random spawner

                    //test movimento con transform
                    var variaForza = Random.Range(launchforce - 2, launchforce + 2);

                    var newball = VR_TrainingBall.Create(prefab_ball, allSpawner[Random.Range(0, allSpawner.Length)].transform.position, variaForza);
                    

                    yield return new WaitForSeconds(variaDelay);
                }

            }

            StartCoroutine(spawnBasicRoutine(delay));
        }

        void UpdateScore(int value)
        {
            currentScene_GameManager.gameScore += value;
        }

        private void OnDisable()
        {
            //qua dovrei fare l'unsubscribe, ma in realtà questo oggetto è sempre in scena quindi evito
        }

    }
}

