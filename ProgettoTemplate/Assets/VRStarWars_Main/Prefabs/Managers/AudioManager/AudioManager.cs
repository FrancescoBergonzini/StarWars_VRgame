using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

namespace SW_VRGame
{
    public class AudioManager : MonoBehaviour
    {
        public enum AudioType
        {
            None =0,

            RobotCutDestroy,
            LaserSwordOn,
            LaserSwordOff,
            Lose,
            Win,
            EnemySpawn,
            RobotSmashed,
            Button,
            Hide,
            Show,
            StartGame,
            PlayerDamage           
            
        }

        // RERFACTOR: nome pi? giusto ? SoundLibraryItem
        [System.Serializable]
        public class SoundLibrary
        {
            public AudioType type;

            public AudioClip clip;
            [Range(0, 1)]
            public float volume;
            [Range(0.1f, 3f)]
            public float pitch;

            public bool loop;

            [HideInInspector]
            public AudioSource source;
        }

        public SoundLibrary[] sounds; //sarebbe da ottimizzare con Dictionary...

        public static AudioManager Instance;

        private void Awake()
        {
            //singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            //
            DontDestroyOnLoad(gameObject);

            foreach (SoundLibrary s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void Play(AudioType type)
        {
            SoundLibrary sound = null;

            foreach (SoundLibrary s in sounds)
            {
                if (s.type == type)
                {
                    sound = s;
                }
            }

            if (sound != null)
            {
                sound.source.Play();

            }
            else
            {
                Debug.LogWarning("Errore, nessun suono con questo nome");
                return;
            }
        }

    }
}