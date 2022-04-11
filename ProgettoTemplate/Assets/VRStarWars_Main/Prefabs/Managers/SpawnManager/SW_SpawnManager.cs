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

        public class WrapperConfig
        {
            SpawnConfigValue copy;
            public WrapperConfig(SpawnConfigValue my_valueStruct)
            {
                copy = new SpawnConfigValue();
                copy.spawnPosition = my_valueStruct.spawnPosition;
                copy.launchforce = my_valueStruct.launchforce;
                copy.delay = my_valueStruct.delay;
                copy.prefab_ball = my_valueStruct.prefab_ball;
                copy.maxEnemyforWave = my_valueStruct.maxEnemyforWave;
                copy.tranform_parent = my_valueStruct.tranform_parent;
            }

            public SpawnConfigValue ReturnValueCopy() { return copy; }
        }

        [Space]
        [SerializeField] TemplateGameManager currentScene_GameManager;
        public Coroutine current_GameLoop = null;

        [Space]
        [SerializeField] GameObject spawn_particle;


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
                    myconfig.delay -= (myconfig.delay / myconfig.maxEnemyforWave);

                    if (myconfig.delay < 0.5)
                        myconfig.delay = 1f;

                    yield return new WaitForSeconds(myconfig.delay);

                    //varietà                  
                    var variaForza = Random.Range(myconfig.launchforce - 2, myconfig.launchforce + 2);

                    //spawn
                    var newball = SW_TrainingBall.Create(myconfig.prefab_ball, myconfig.spawnPosition[Random.Range(0, myconfig.spawnPosition.Length)].transform.position, variaForza, myconfig.tranform_parent);

                    //Particle
                    var particle = Instantiate(spawn_particle, newball.transform);
                    Destroy(particle, 5);

                    myconfig.maxEnemyforWave--;
                   
                }

                Debug.LogWarning("Terminato ciclo wave");
                yield return new WaitForSeconds(20f);
                EndGameClear(myconfig);
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

        void EndGameClear(SpawnConfigValue myconfig)
        {
            current_GameLoop = null;
            //
            foreach (Transform child in myconfig.tranform_parent)
                Destroy(child.gameObject);
        }


    }
}

