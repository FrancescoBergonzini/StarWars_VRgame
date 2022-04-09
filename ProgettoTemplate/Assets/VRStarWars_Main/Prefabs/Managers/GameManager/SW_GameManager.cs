using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace SW_VRGame
{
    //evento Unity, publisher
    public class Event_Score : UnityEvent<int> { }

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
        [SerializeField] TextMeshProUGUI UI_score;
        [SerializeField] SW_SpawnManager my_SpawnManager; //qua sarebbe meglio usare UnityEvent

        //Score
        //Gamemanager deve ascoltare le chiamate da CutBlade e Cestino e aumentare lo score quando indicato
        public int gameScore = 0;

        protected override void OnAwake()
        {
            SW_CutBladeLogic.Instance.Event_UpdateScoreBlade.AddListener(UpdateScore);
            SW_Canestro.Instance.Event_UpdateScoreCanestro.AddListener(UpdateScore);
        }


        private void Update()
        {
            //
            UI_score.text = gameScore.ToString();
        }

        private void Start()
        {
            my_SpawnManager.Test_SpawnBasicRoutine();
        }

        //
        void UpdateScore(int value)
        {
            gameScore += value;
        }


    }
}

