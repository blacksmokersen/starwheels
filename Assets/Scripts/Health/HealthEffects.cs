using UnityEngine;

namespace Health
{
    public class HealthEffects : MonoBehaviour
    {
        public ParticleSystem[] LifeBursts;
        public int NumberOfParticles = 300;


        public void HealthParticlesManagement(int health)
        {
            LifeBursts[0].Play();
        }
    }
}
