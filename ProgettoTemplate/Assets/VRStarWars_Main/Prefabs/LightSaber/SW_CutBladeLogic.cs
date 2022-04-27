using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioItem = SW_VRGame.AudioManager.AudioType;
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
        public System.Action<float, float> Event_ApticInput;

        private void OnCollisionEnter(Collision collision)
        {       
            if (collision.gameObject.tag != Tags.Sliceable)
                return;

            //prima dello slice
            //staccare le mesh child
            if (collision.gameObject.TryGetComponent(out SW_RobotPiece disabble))
            {
                disabble.DisassemblePieces();
            }

            //eseguo API cut
            var subsliced = SliceWithCollision.Slice(transform.up, collision, mat_cubeSlice);

            //parento i pezzi al Bin cosi poi posso distruggerli
            foreach(GameObject subMesh in subsliced)
            {
                subMesh.transform.parent = SW_GameManager.Instance.Environment.transform;
                subMesh.AddComponent<SW_RobotCutted>();

                //PARTICLE
                Destroy(Instantiate(_robotShockParticle, subMesh.transform), 3);
            }

            #region MultiCut, tolto per ottimizzazione
                /*
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
                */
                #endregion

            //lancio evento +1 score
            Event_UpdateScoreBlade.Invoke(1);

            //AUDIO 
            AudioManager.Instance.Play(AudioItem.RobotCutDestroy);

            //APCTIC EFFECT
            Event_ApticInput(0.75f, 0.5f);
            

        }
    }
}

