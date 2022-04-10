using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class SW_SpawnManager : Singleton<SW_SpawnManager>
    {
        [Header("Game loop elements")]
        [SerializeField] Transform[] spawnPosition;
        [SerializeField] float launchforce;
        [SerializeField] float delay;
        [SerializeField] SW_TrainingBall prefab_ball;
        [SerializeField] int maxEnemyforWave;
        [SerializeField] Transform tranform_parent;

        [Space]
        [SerializeField] TemplateGameManager currentScene_GameManager;

        [SerializeField] Coroutine current_GameLoop = null;

        protected override void OnAwake()
        {
            //
            if (currentScene_GameManager == null)
            {
                currentScene_GameManager = FindObjectOfType<TemplateGameManager>();
            }

            base.OnAwake();
        }


        public void Test_SpawnBasicRoutine() //per avviare una nuova partita, questo va resettato
        {
            if (current_GameLoop != null)
                return;

            IEnumerator spawnBasicRoutine()
            {
                while (maxEnemyforWave > 0)
                {
                    //wait
                    delay = Random.Range(delay - 0.5f, delay + 0.5f);
                    delay -= (delay / maxEnemyforWave);

                    if (delay < 0.5)
                        delay = 0.5f;

                    yield return new WaitForSeconds(delay);

                    //varietà                  
                    var variaForza = Random.Range(launchforce - 2, launchforce + 2);

                    //spawn
                    var newball = SW_TrainingBall.Create(prefab_ball, spawnPosition[Random.Range(0, spawnPosition.Length)].transform.position, variaForza, tranform_parent);

                    maxEnemyforWave--;
                   
                }

                Debug.LogWarning("Terminato ciclo wave");
                //despown pezzi rimasti

            }

            current_GameLoop = StartCoroutine(spawnBasicRoutine());
        }

        public void StopLoopCoroutine()
        {
            if(current_GameLoop != null)
            {
                StopCoroutine(current_GameLoop);
                current_GameLoop = null;
            }
        }


    }
}

