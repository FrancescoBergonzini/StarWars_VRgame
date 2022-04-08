using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SW_VRGame
{
    //evento Unity, publisher
    public class Event_Score : UnityEvent<int> { }

    /// <summary>
    /// Classe con logica per il cut delle mesh + update dello score
    /// </summary>
    public class SW_CutBladeLogic : Singleton<SW_CutBladeLogic>
    {
        [SerializeField] Material mat_cubeSlice;

        //evento con tipo il delegate indicato sopra
        public Event_Score UpdatescoreEnabler = new Event_Score();

        //particle per ora qua
        [SerializeField] GameObject _robotShockParticle;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != "Sliceable")
                return;

            //prima dello slice
            //staccare le mesh child
            //applicarle una forza esplosiva
            //REFACToRING
            if(collision.gameObject.TryGetComponent(out SW_RobotPiece disabble))
            {
                disabble.DisassemblePieces();
            }

            var subsliced = SliceWithCollision.Slice(transform.up, collision, mat_cubeSlice);

            if (collision.gameObject.TryGetComponent(out SW_RobotCutted cutted))
            {
                //se trova gi� il component significa che � una mesh gi� tagliata
                if(cutted.sliceCounter > 0)
                {                   
                    foreach (var slice in subsliced)
                    {
                        slice.AddComponent<SW_RobotCutted>().sliceCounter = cutted.sliceCounter - 1;
                    }
                }               
            }
            else
            {
                //mesh tagliata per la prima volta
                foreach (GameObject slice in subsliced)
                {
                    //PARTICLE
                    Destroy(Instantiate(_robotShockParticle, slice.transform), 3);
                }
                
                foreach (var slice in subsliced)
                {
                    //crea due nuove mesh tagliabili
                    slice.AddComponent<SW_RobotCutted>();
                }

                //assegna punti
                UpdatescoreEnabler.Invoke(1); //passo 1, come parametro
            }

        }
    }
}

