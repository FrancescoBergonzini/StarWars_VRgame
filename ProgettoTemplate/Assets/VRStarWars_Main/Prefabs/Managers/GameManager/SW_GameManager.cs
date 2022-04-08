using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace SW_VRGame
{

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

        private void Update()
        {
            //
            UI_score.text = gameScore.ToString();
        }

        private void Start()
        {
            my_SpawnManager.Test_SpawnBasicRoutine();
        }

    }
}

