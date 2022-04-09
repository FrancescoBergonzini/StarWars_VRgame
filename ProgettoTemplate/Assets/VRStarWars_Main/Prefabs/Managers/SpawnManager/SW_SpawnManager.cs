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

        //
        [Space]      
        public bool game_loop;
        Coroutine _game_loop = null; //null quando gioco � non avviato


        protected override void OnAwake()
        {
            //
            if (currentScene_GameManager == null)
            {
                currentScene_GameManager = FindObjectOfType<TemplateGameManager>();
            }

            base.OnAwake();
        }

        private void Update()
        {
            //debug
            if (_game_loop == null)
            {
                game_loop = false;
            }
            else game_loop = true;
        }

        public void Test_SpawnBasicRoutine()
        {
            if (_game_loop != null)
                return;

            IEnumerator spawnBasicRoutine()
            {
                int enemySpawn = 0;           

                while (enemySpawn < maxEnemyforWave)
                {
                    Debug.Log("Spawn una mesh");
                    var variaDelay = Random.Range(delay - 1f, delay + 1f);
                    //choose random spawner

                    //test movimento con transform
                    var variaForza = Random.Range(launchforce - 2, launchforce + 2);

                    var newball = SW_TrainingBall.Create(prefab_ball, spawnPosition[Random.Range(0, spawnPosition.Length)].transform.position, variaForza, tranform_parent);

                    enemySpawn++;

                    //implementa aumento difficolt�
                    delay -= (delay / (maxEnemyforWave - enemySpawn));

                    yield return new WaitForSeconds(variaDelay);
                }

                Debug.LogWarning("Terminato ciclo wave");
                //despown pezzi rimasti

                _game_loop = null;

            }

            _game_loop = StartCoroutine(spawnBasicRoutine());
        }

        //aggiornamento Score

        private void OnDisable()
        {
            //qua dovrei fare l'unsubscribe, ma in realt� questo oggetto � sempre in scena quindi evito
        }

        //collider child gestisce distruzione robot nel caso player non li prenda
        private void OnCollisionEnter(Collision collision)
        {

            Debug.Log("Colliso nel box");

            //se sono robot
            if(collision.gameObject.tag == Tags.Sliceable)
            {
                //danneggia il player

                //distruggi il robot
                Destroy(collision.gameObject);
            }


        }

    }
}

