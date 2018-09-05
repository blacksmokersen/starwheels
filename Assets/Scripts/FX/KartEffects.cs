using UnityEngine;

namespace FX
{
    public class KartEffects : BaseKartComponent
    {
        [HideInInspector] public ParticleSystem smokeLeftWheel;
        [HideInInspector] public ParticleSystem smokeRightWheel;

        public ParticleSystem[] LifeBursts;
        [Space(10)]
        [Header("Particles")]
        public ParticleSystem MainJumpParticles;
        public ParticleSystem JumpReloadParticles;

        private int numberOfParticles = 300;

        private new void Awake()
        {
            base.Awake();
            StopSmoke();

            // Events
            kartEvents.OnJump += MainJumpParticlesEmit;
            kartEvents.OnDoubleJumpReset += ReloadJumpParticlesEmit;

            kartEvents.OnHealthLoss += HealthParticlesManagement;

            kartEvents.OnDriftStart += StartSmoke;
            kartEvents.OnDriftStart += () => SetWheelsColor(Color.white);
            kartEvents.OnDriftEnd += StopSmoke;
            kartEvents.OnDriftEnd += () => SetWheelsColor(Color.white);

            kartEvents.OnDriftOrange += () => SetWheelsColor(Color.yellow);
            kartEvents.OnDriftRed += () => SetWheelsColor(Color.red);
            kartEvents.OnDriftBoost += () => SetWheelsColor(Color.green);
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
            LifeBursts[0].Play();
        }

        public void MainJumpParticlesEmit()
        {
            MainJumpParticles.Emit(numberOfParticles);
        }

        public void ReloadJumpParticlesEmit()
        {
            //  JumpReloadParticles.Emit(numberOfParticles);
        }

        public void SetWheelsColor(Color color)
        {
            var leftWheelMain = smokeLeftWheel.main;
            var rightWheelMain = smokeRightWheel.main;

            leftWheelMain.startColor = color;
            rightWheelMain.startColor = color;
        }
    }
}
