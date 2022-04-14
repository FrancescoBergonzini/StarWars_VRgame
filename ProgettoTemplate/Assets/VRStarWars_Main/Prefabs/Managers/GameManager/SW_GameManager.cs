using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

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
        private int bestScore = 0;

        [Space]
        [SerializeField] SW_SpawnManager my_SpawnManager; //qua sarebbe meglio usare UnityEvent

        //Score
        //Gamemanager deve ascoltare le chiamate da CutBlade e Cestino e aumentare lo score quando indicato
        public int gameScore = 0;
     
        [Header("Variabili spawn loop")]
        [SerializeField] SW_SpawnManager.SpawnConfigValue config;
        [SerializeField] SW_SpawnManager.SpawnConfigValue clone_config;


        protected override void OnAwake()
        {
            SW_CutBladeLogic.Instance.Event_UpdateScoreBlade.AddListener(UpdateScore);
            SW_Canestro.Instance.Event_UpdateScoreCanestro.AddListener(UpdateScore);
            SW_CutBladeLogic.Instance.Event_StartGame.AddListener(StartNewGame);

        }


        protected void Update()
        {
            #region test_debug
            /*
            if (METTIGameInPausa)
            {
                PauseGame();
                METTIGameInPausa = false;

                //questo realmente va metto true quando il gioco è in pausa
                GameIsInPause = true;

                my_SpawnManager.StopLoopCoroutine();

            }

            if (RESUMEGameInPausa)
            {
                ResumeGame();
                RESUMEGameInPausa = false;

                GameIsInPause = false;

                my_SpawnManager.Test_SpawnBasicRoutine();

            }
            */
            #endregion
        }


        public void StartNewGame()
        {
            //nuova ondata
            clone_config = new SW_SpawnManager.WrapperConfig(config).ReturnValueCopy();

            //clone_config = copy;
            my_SpawnManager.Test_SpawnBasicRoutine(clone_config);
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
            gameScore += value;

            //UI
            UI_score.text = gameScore.ToString();
        }

        public override void UpdateScoreEndGame()
        {
            //
            bestScore = bestScore > gameScore ? bestScore : gameScore;
            UI_bestScore.text = $"Best score: {bestScore}";

            gameScore = 0;
            UI_score.text = "Cut the ROBOT\nto START";



        }



    }
}

