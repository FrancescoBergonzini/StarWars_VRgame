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
            [Space]
            public GameObject start_Cube;
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
                copy.start_Cube = my_valueStruct.start_Cube;
            }

            public SpawnConfigValue ReturnValueCopy() { return copy; }
        }

        [Space]
        [SerializeField] TemplateGameManager currentScene_GameManager;
        public Coroutine current_GameLoop = null;

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
            if (current_GameLoop != null)
                return;

            IEnumerator spawnBasicRoutine(SpawnConfigValue myconfig)
            {

                while (myconfig.maxEnemyforWave > 0)
                {

                    //wait                   
                    //myconfig.delay -= (myconfig.delay / myconfig.maxEnemyforWave);

                    //if (myconfig.delay < 0.5)
                        //myconfig.delay = 1f;

                    yield return new WaitForSeconds(myconfig.delay);

                    //varietà                  
                    //var variaForza = Random.Range(myconfig.launchforce - 2, myconfig.launchforce + 2);

                    //spawn
                    var newball = SW_TrainingBall.Create(myconfig.prefab_ball, myconfig.spawnPosition[Random.Range(0, myconfig.spawnPosition.Length)].transform.position, myconfig.launchforce, myconfig.tranform_parent);

                    //Particle
                    var particle = Instantiate(spawn_particle, newball.transform);
                    Destroy(particle, 5);

                    myconfig.maxEnemyforWave--;
                   
                }

                //
                EndGameClear(myconfig, 15);

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


        //GAME OVER COROUTINES
        void EndGameClear(SpawnConfigValue myconfig, float timeTowait)
        {
            IEnumerator endGameClear()
            {
                yield return new WaitForSeconds(timeTowait);

                if (myconfig.tranform_parent != null && myconfig.tranform_parent.childCount > 0)
                {
                    foreach (Transform child in myconfig.tranform_parent)
                        Destroy(child.gameObject);

                    endTheGame(false);
                }

                
            }

            StartCoroutine(endGameClear());
        }


        public void EndGameforGameOver(Transform pausable, GameObject startCube)
        {
            //questa chiamata deve fermare spawnBasicRoutine
            if(current_GameLoop != null)
            StopCoroutine(current_GameLoop);

            if (pausable != null && pausable.childCount > 0)
            {
                foreach (Transform child in pausable)
                    Destroy(child.gameObject);

                endTheGame(true);
            }         

        }


    }
}

