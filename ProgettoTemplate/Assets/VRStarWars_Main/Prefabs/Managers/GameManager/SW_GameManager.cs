using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AudioItem = SW_VRGame.AudioManager.AudioType;

namespace SW_VRGame
{
    //evento Unity, publisher
    public class Event_Score : UnityEvent<int> { }
    public class Event_Start : UnityEvent { }


    public static class Tags
    {
        public const string LightSword = "Sword";
        public const string Sliceable = "Sliceable";
    }

    public class SW_GameManager : TemplateGameManager
    {
        [Header("UI direct ref")]
        [SerializeField] TextMeshProUGUI UI_score;
        [SerializeField] TextMeshProUGUI UI_bestScore;
        [SerializeField] Image[] life_icons;
        private int bestScore = 0;

        [Space]
        [SerializeField] SW_EndGameCollider my_colliderGAmeOver;

        [Space]
        [SerializeField] SW_SpawnManager my_SpawnManager; 

        //Score
        //Gamemanager deve ascoltare le chiamate da CutBlade e Cestino e aumentare lo score quando indicato
        int gameScore = 0;
        int playerLife = 3;
        public SW_Start_Cube start_cube; 
        public Vector3 pos_GameOver;
        public GameObject player;
     
        [Header("Variabili spawn loop")]
        [SerializeField] SW_SpawnManager.SpawnConfigValue config;
        [SerializeField] SW_SpawnManager.SpawnConfigValue clone_config;

        //
        //NOTA: Il GameManager implementa i suoi metodi tramite gli eventi

        protected override void OnAwake()
        {
            //gestione score
            SW_CutBladeLogic.Instance.Event_UpdateScoreBlade.AddListener(UpdateScore);
            SW_Canestro.Instance.Event_UpdateScoreCanestro.AddListener(UpdateScore);

            //start new game
            start_cube.startnewWave += StartNewGame;

            //damage player
            my_colliderGAmeOver.checkRobotCollision += DamagePlayer;

            //game End
            SW_SpawnManager.Instance.endTheGame += UpdateScoreEndGame;

        }


        public void StartNewGame()
        {

            SW_SpawnManager.Instance.StopLoopCoroutine();
            //nuova ondata
            clone_config = new SW_SpawnManager.WrapperConfig(config).ReturnValueCopy();

            my_SpawnManager.Test_SpawnBasicRoutine(clone_config);

            UI_score.text = "CUT THE\n ROBOTS!";

            //spegni start_cube
            start_cube.ActiveMyself = false;

            //AUDIO
            AudioManager.Instance.Play(AudioItem.StartGame);
        }

        public override void ResumeGame()
        {
            if(my_SpawnManager.current_GameLoop == null && !start_cube.ActiveMyself)
            {
                my_SpawnManager.Test_SpawnBasicRoutine(clone_config);
                base.ResumeGame();
            }

            //UI
            if(gameScore == 0)
            {
                UI_score.text = "Cut the ROBOT\nto START";
            }
            else
            UI_score.text = gameScore.ToString();

        }

        public override void PauseGame()
        {

            my_SpawnManager.StopLoopCoroutine();
            base.PauseGame();

            //UI
            UI_score.text = "PAUSE";

        }

        //damage
        void DamagePlayer()
        {
            if(playerLife > 1)
            {
                playerLife--;
                life_icons[playerLife].enabled = false;
            }
            else
            {
                //game Over
                player.transform.position = pos_GameOver;
                //Destroy(SW_LightSaber.Instance.gameObject);

            }

        }

        //

        //Score metodi
        void UpdateScore(int value)
        {
            if (my_SpawnManager.current_GameLoop == null || !SW_SpawnManager.Instance.waveWorking)
                return;

            gameScore += value;

            //UI
            UI_score.text = gameScore.ToString();
        }

        /// <summary>
        /// //Passare TRUE se si perde per life loss
        /// </summary>
        /// <param name="isGameOver"></param>
        public override void UpdateScoreEndGame(bool isGameOver = false) 
        {
            if (!isGameOver)
            {
                UI_score.text = "GOOD GAME\nCut the ROBOT\nto START";

                //AUDIO
                AudioManager.Instance.Play(AudioItem.Win);
            }
            else
            {
                UI_score.text = "GAME OVER!\nCut the ROBOT\nto START";

                //AUDIO
                AudioManager.Instance.Play(AudioItem.Lose);
            }
                

            bestScore = bestScore > gameScore ? bestScore : gameScore;
            UI_bestScore.text = $"Best score: {bestScore}";

            //reset
            gameScore = 0;
            playerLife = 3;

            foreach (Image icon in life_icons)
            {
                icon.enabled = true;
            }

            //restore start_cube
            start_cube.ActiveMyself = true;

        }

        //
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}

