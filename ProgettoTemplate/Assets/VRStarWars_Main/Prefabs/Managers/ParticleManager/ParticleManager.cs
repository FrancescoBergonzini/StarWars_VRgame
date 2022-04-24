using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SW_VRGame
{

    public class ParticleManager : MonoBehaviour
    {
        public enum ParticleEffectType
        {
            None = 0,

        }

        [System.Serializable]
        public class ParticleLibraryItem
        {
            public ParticleEffectType type;

            public GameObject prefab_particle;
            [Range(-1f, 10f)]
            public float Lifetime;

            [HideInInspector]
            public ParticleSystem internalParticle;
        }


        public ParticleLibraryItem[] particles; // 

        public static ParticleManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

        }

        public GameObject InstantiateParticle(ParticleEffectType type, Vector3 position)
        {
            ParticleLibraryItem particle = null;

            foreach (ParticleLibraryItem p in particles)
            {
                if (p.type == type)
                {
                    particle = p;
                }
            }

            if (particle != null)
            {
                //istanzia
                GameObject part = Instantiate(particle.prefab_particle, position, Quaternion.identity, this.transform);

                if (particle.Lifetime > 0)
                    Destroy(part, particle.Lifetime);

                return part;
            }
            else
            {
                Debug.LogWarning("Errore, nessun particellare con questo nome");
                return null;
            }
        }
    }
}
