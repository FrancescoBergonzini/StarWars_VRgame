using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SW_VRGame
{
    public class UpdateScore : UnityEvent<int> { }

    public class SW_GameManger : TemplateGameManager
    {
        //questa classe funge da Publisher per getire lo stato del gioco
        public UpdateScore MyUpdateScore = new UpdateScore();

        protected override void OnAwake()
        {
            //base.OnAwake(); //superlfuo tanto è vuoto...
            
        }


    }
}

