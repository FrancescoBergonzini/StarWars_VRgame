using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Tutti i component che possono entrare in uno stato di pausa devono implementare questa interfaccia
/// </summary>
public interface IPausable
{
    //Il component può essere messo in pausa?
    bool IsPausable { get; }

    //Implementare per il component la logica di pausa e resume dalla pausa
    void Pause(bool Pause);
}
