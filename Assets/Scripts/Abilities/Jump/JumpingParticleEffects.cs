using UnityEngine;

namespace Abilities.Jump
{
    public class JumpingParticleEffects : MonoBehaviour
    {
        [Header("Particles")]
        public ParticleSystem MainJumpParticles;

        [Header("Settings")]
        public int ParticlesToEmit = 300;

        public void MainJumpParticlesEmit()
        {
            MainJumpParticles.Emit(ParticlesToEmit);
        }
    }
}
