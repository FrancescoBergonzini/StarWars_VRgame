using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{
    public class SW_Player : Item
    {
        [Serializable]
        public struct ItemStat
        {
            public int MaxHealth;
            public ItemType ItemType;
        }

        [SerializeField] ItemStat config;
        //non uso damage e score

        private void Awake()
        {
            InizializeConfigPlayer();
        }

        private void InizializeConfigPlayer()
        {
            config.ItemType = ItemType;
            config.MaxHealth = MaxHealth;

            CurrHealth = MaxHealth;
        }
    }
}

