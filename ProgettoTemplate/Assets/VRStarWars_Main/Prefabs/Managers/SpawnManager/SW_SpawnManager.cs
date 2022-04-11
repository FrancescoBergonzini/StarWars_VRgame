using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    

    public class SW_SpawnManager : Singleton<SW_SpawnManager>
    {
        [System.Serializable]
        public class SpawnConfigValue
        {
            public Transform[] spawnPosition;
            public float launchforce;
            public float delay;
            public SW_TrainingBall prefab_ball;
            public int maxEnemyforWave;
            public Transform tranform_parent;
        }

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



        public void Test_SpawnBasicRoutine(SpawnConfigValue myconfig) //per avviare una nuova partita, questo va resettato
        {
            if (current_GameLoop != null || myconfig.maxEnemyforWave <= 0)
                return;

            IEnumerator spawnBasicRoutine(SpawnConfigValue myconfig)
            {
                while (myconfig.maxEnemyforWave > 0)
                {

                    //wait
                    myconfig.delay = Random.Range(myconfig.delay - 0.5f, myconfig.delay + 0.5f);
                    myconfig.delay -= (myconfig.delay / myconfig.maxEnemyforWave);

                    if (myconfig.delay < 0.5)
                        myconfig.delay = 0.5f;

                    yield return new WaitForSeconds(myconfig.delay);

                    //varietà                  
                    var variaForza = Random.Range(myconfig.launchforce - 2, myconfig.launchforce + 2);

                    //spawn
                    var newball = SW_TrainingBall.Create(myconfig.prefab_ball, myconfig.spawnPosition[Random.Range(0, myconfig.spawnPosition.Length)].transform.position, variaForza, myconfig.tranform_parent);

                    myconfig.maxEnemyforWave--;
                   
                }

                Debug.LogWarning("Terminato ciclo wave");
                //despown pezzi rimasti

            }

            current_GameLoop = StartCoroutine(spawnBasicRoutine(myconfig));
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

