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
        public bool waveWorking;

        [Space]
        [SerializeField] GameObject spawn_particle;

        //
        public System.Action<bool> endTheGame;

        protected override void OnAwake()
        {
            base.OnAwake();
        }


        public void Test_SpawnBasicRoutine(SpawnConfigValue myconfig) //per avviare una nuova partita, questo va resettato
        {
            IEnumerator spawnBasicRoutine(SpawnConfigValue myconfig)
            {
                waveWorking = true;
                int cont = 0;

                while (myconfig.maxEnemyforWave > 0)
                {
                    //aumenta difficoltà aumentando velocità e diminuendo delay
                    var new_lounchforce = myconfig.launchforce;
                    var new_delay = myconfig.delay;

                    //molto rozzo...
                    if(cont > 4)
                    {
                        new_lounchforce += 0.5f;
                        new_delay -= 0.5f;
                    }
                    else if(cont > 8)
                    {
                        new_lounchforce += 0.5f;
                        new_delay -= 0.5f;
                    }
                    else if(cont > 12)
                    {
                        new_lounchforce += 0.5f;
                        new_delay -= 0.5f;
                    }
                    else if(cont > 16)
                    {
                        new_lounchforce += 0.5f;
                        new_delay -= 0.5f;
                    }


                    yield return new WaitForSeconds(new_delay);
                    

                    //spawn
                    var newball = SW_TrainingBall.Create(myconfig.prefab_ball, myconfig.spawnPosition[Random.Range(0, myconfig.spawnPosition.Length)].transform.position, new_lounchforce, myconfig.tranform_parent);

                    //Particle
                    var particle = Instantiate(spawn_particle, newball.transform);
                    Destroy(particle, 5);

                    myconfig.maxEnemyforWave--;
                }

                //
                yield return new WaitForSeconds(10);

                if (myconfig.tranform_parent != null && myconfig.tranform_parent.childCount > 0)
                {
                    foreach (Transform child in myconfig.tranform_parent)
                        Destroy(child.gameObject);

                    endTheGame(false);
                }

                waveWorking = false;

            }

            current_GameLoop = StartCoroutine(spawnBasicRoutine(myconfig));
        }

        public void StopLoopCoroutine()
        {
            if(current_GameLoop != null)
            {
                StopCoroutine(current_GameLoop);
                current_GameLoop = null;
                waveWorking = false;
            }
        }

    }
}

