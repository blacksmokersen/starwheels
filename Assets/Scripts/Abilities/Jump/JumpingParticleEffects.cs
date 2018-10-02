using UnityEngine;

namespace Abilities.Jump
{
    public class JumpingParticleEffects : MonoBehaviour
    {
        [Header("Particles")]
        public ParticleSystem MainJumpParticles;
        public ParticleSystem AbilityReloadParticles;

        [Header("Settings")]
        public int ParticlesToEmit = 300;

        public void MainJumpParticlesEmit()
        {
            MainJumpParticles.Emit(ParticlesToEmit);
        }

        public void ReloadAbilityParticlesEmit()
        {
            AbilityReloadParticles.Emit(ParticlesToEmit);
        }
    }
}
