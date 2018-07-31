using UnityEngine;


namespace FX
{
    public class KartEffects : BaseKartComponent
    {
        [HideInInspector] public ParticleSystem smokeLeftWheel;
        [HideInInspector] public ParticleSystem smokeRightWheel;

        public ParticleSystem[] Lifes;
        public ParticleSystem[] LifeBursts;
        [Space(10)]
        public ParticleSystem MainJumpParticles;
        public ParticleSystem JumpReloadParticles;
        public int NumberOfParticles = 300;

        private new void Awake()
        {
            base.Awake();
            kartEvents.OnJump += MainJumpParticlesEmit;
            kartEvents.OnDoubleJumpReset += ReloadJumpParticlesEmit;
            kartEvents.OnDriftBoost += StartSmoke;
            kartEvents.OnDriftReset += StopSmoke;
        }

        public void StopSmoke()
        {
            smokeLeftWheel.Stop(true);
            smokeRightWheel.Stop(true);
        }
        public void StartSmoke()
        {
            if (!smokeLeftWheel.isPlaying)
                smokeLeftWheel.Play(true);
            if (!smokeRightWheel.isPlaying)
                smokeRightWheel.Play(true);
        }

        public void HealthParticlesManagement(int health)
        {
            Lifes[health].Stop(true);
            LifeBursts[health].Play();
        }

        public void ResetLives()
        {
            foreach (ParticleSystem ps in Lifes)
            {
                ps.Play();
            }
        }

        public void MainJumpParticlesEmit()
        {
            MainJumpParticles.Emit(NumberOfParticles);
        }

        public void ReloadJumpParticlesEmit()
        {
            JumpReloadParticles.Emit(NumberOfParticles);
        }       

        public void SetWheelsColor(Color color)
        {
            var main = smokeLeftWheel.main;
            var main2 = smokeRightWheel.main;

            main.startColor = color;
            main2.startColor = color;
        }
    }
}

