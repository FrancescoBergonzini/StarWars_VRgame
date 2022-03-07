using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType { ENEMY, PLAYER, OBJECT, WEAPON}


/// <summary>
/// Tutti gli oggetti/item interattivi in scena dovrebbero estendere questa classe
/// </summary>
public class Item : MonoBehaviour
{
    //Tipo di item
    public ItemType ItemType { get; protected set; }

    //Vita massima
    public int MaxHealth { get; protected set; } = 0;

    //Vita corrente
    public int CurrHealth { get; protected set; } = 0;

    //Danno che possono infliggere
    public int Damage { get; protected set; } = 0;

    //Punteggio che trasferiscono a chi li usa/distrugge
    public int Score { get; protected set; } = 0;

    //Parametri e info dell'item
    public Dictionary<string, object> Params { get; protected set; } = new Dictionary<string, object>();
}
