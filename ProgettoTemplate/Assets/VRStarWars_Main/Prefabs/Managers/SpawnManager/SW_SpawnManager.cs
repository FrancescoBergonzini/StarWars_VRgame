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

        [Space]
        [SerializeField] TemplateGameManager currentScene_GameManager;

        //
        [Space]      
        public bool game_loop;
        Coroutine _game_loop = null; //null quando gioco è non avviato


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

                    var newball = SW_TrainingBall.Create(prefab_ball, spawnPosition[Random.Range(0, spawnPosition.Length)].transform.position, variaForza);

                    enemySpawn++;

                    //implementa aumento difficoltà

                    yield return new WaitForSeconds(variaDelay);
                }

                Debug.LogWarning("Terminato ciclo wave");
                _game_loop = null;

            }

            _game_loop = StartCoroutine(spawnBasicRoutine());
        }

        //aggiornamento Score

        private void OnDisable()
        {
            //qua dovrei fare l'unsubscribe, ma in realtà questo oggetto è sempre in scena quindi evito
        }

    }
}

