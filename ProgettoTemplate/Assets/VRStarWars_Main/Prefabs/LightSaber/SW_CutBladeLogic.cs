using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SW_VRGame
{

    /// <summary>
    /// Classe con logica per il cut delle mesh + update dello score
    /// </summary>
    public class SW_CutBladeLogic : Singleton<SW_CutBladeLogic>
    {
        [SerializeField] Material mat_cubeSlice;

        //particle per ora qua
        [SerializeField] GameObject _robotShockParticle;

        //genera evento update score al cut del nemico
        public Event_Score Event_UpdateScoreBlade = new Event_Score();
        public Event_Start Event_StartGame = new Event_Start();

        private void OnCollisionEnter(Collision collision)
        {       

            if (collision.gameObject.tag != "Sliceable")
                return;

            //SISTEMAS
            if(SW_SpawnManager.Instance.current_GameLoop == null)
            {
                Event_StartGame.Invoke();
            }
            

            //prima dello slice
            //staccare le mesh child
            if (collision.gameObject.TryGetComponent(out SW_RobotPiece disabble))
            {
                disabble.DisassemblePieces();
            }

            var subsliced = SliceWithCollision.Slice(transform.up, collision, mat_cubeSlice);

            //parento i pezzi al Bin cosi poi posso distruggerli
            foreach(GameObject subMesh in subsliced)
            {
                subMesh.transform.parent = SW_GameManager.Instance.Environment.transform;
            }


            if (collision.gameObject.TryGetComponent(out SW_RobotCutted cutted))
            {
                //se trova già il component significa che è una mesh già tagliata
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

                //lancio evento +1 score
                Event_UpdateScoreBlade.Invoke(1);

            }

        }
    }
}

