using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace SW_VRGame
{
    public class SW_TutorialGameManager : TemplateGameManager
    {
        [SerializeField] TextMeshProUGUI UI_score;

        private void Update()
        {
            //
            UI_score.text = gameScore.ToString();
        }

    }
}

