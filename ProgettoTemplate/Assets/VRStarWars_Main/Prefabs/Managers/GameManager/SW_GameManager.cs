using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SW_VRGame
{
    //evento Unity, publisher
    public class Event_Score : UnityEvent<int> { }
    public class Event_Start : UnityEvent { }

    public static class Layers
    {
        
    }

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
        [SerializeField] SW_SpawnManager my_SpawnManager; //qua sarebbe meglio usare UnityEvent

        //Score
        //Gamemanager deve ascoltare le chiamate da CutBlade e Cestino e aumentare lo score quando indicato
        public int gameScore = 0;
        public int playerLife;
     
        [Header("Variabili spawn loop")]
        [SerializeField] SW_SpawnManager.SpawnConfigValue config;
        [SerializeField] SW_SpawnManager.SpawnConfigValue clone_config;


        protected override void OnAwake()
        {
            SW_CutBladeLogic.Instance.Event_UpdateScoreBlade.AddListener(UpdateScore);
            SW_Canestro.Instance.Event_UpdateScoreCanestro.AddListener(UpdateScore);
            SW_CutBladeLogic.Instance.Event_StartGame.AddListener(StartNewGame);
            my_colliderGAmeOver.checkRobotCollision += DamagePlayer;

        }


        public void StartNewGame()
        {
            //nuova ondata
            clone_config = new SW_SpawnManager.WrapperConfig(config).ReturnValueCopy();

            //clone_config = copy;
            my_SpawnManager.Test_SpawnBasicRoutine(clone_config);

            RestorePlayerHealth();
        }

        //
        public override void ResumeGame()
        {
            if(my_SpawnManager.current_GameLoop == null)
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
            if(my_SpawnManager.current_GameLoop != null)
            {
                my_SpawnManager.StopLoopCoroutine();
                base.PauseGame();
            }

            //UI
            UI_score.text = "PAUSE";

        }

        //
        void UpdateScore(int value)
        {
            if (my_SpawnManager.current_GameLoop == null)
                return;

            gameScore += value;

            //UI
            UI_score.text = gameScore.ToString();
        }

        void DamagePlayer()
        {
            playerLife--;
            life_icons[playerLife].enabled = false;
            
            if(playerLife <= 0)
            {
                //game Over
                my_SpawnManager.EndGameforGameOver();

                //UI
                UpdateScoreEndGame();
                UI_score.text = "GAME OVER\nCut the ROBOT\nto START";

            }
        }

        public override void UpdateScoreEndGame()
        {
            //
            bestScore = bestScore > gameScore ? bestScore : gameScore;
            UI_bestScore.text = $"Best score: {bestScore}";

            gameScore = 0;
            UI_score.text = "GOOD GAME\nCut the ROBOT\nto START";

        }

        //
        public void RestorePlayerHealth()
        {
            playerLife = 3;

            foreach(Image icon in life_icons)
            {
                icon.enabled = true;
            }
        }



    }
}

