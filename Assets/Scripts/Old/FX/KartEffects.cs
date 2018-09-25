using Photon.Pun;
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
        public ParticleSystem AbilityReloadParticles;
        public ParticleSystem AimParticles;

        private int numberOfParticles = 300;
        [SerializeField] private float aimEffectAngle = 45;

        private new void Awake()
        {
            base.Awake();
            StopSmoke();
            AimParticles.Stop(true);

            // Events
            kartEvents.OnAbilityUse += MainJumpParticlesEmit;
            kartEvents.OnAbilityReset += ReloadAbilityParticlesEmit;

            kartEvents.OnHealthLoss += HealthParticlesManagement;
            kartEvents.OnCameraTurnStart += AimEffects;

            kartEvents.OnDriftStart += StartSmoke;
            kartEvents.OnDriftStart += () => SetWheelsColor(Color.white);
            kartEvents.OnDriftEnd += StopSmoke;

            kartEvents.OnDriftOrange += () => SetWheelsColor(Color.yellow);
            kartEvents.OnDriftRed += () => SetWheelsColor(Color.red);
            kartEvents.OnDriftBoostStart += () => SetWheelsColor(Color.green);
            kartEvents.OnDriftBoostEnd += StopSmoke;
        }

        public void AimEffects(float aimAxisH,float aimAxisV)
        {
            if(Mathf.Abs(aimAxisV) > 0.1)
            {
                AimParticles.Play(true);
                AimParticles.transform.localEulerAngles = new Vector3(0, aimAxisH, 0) * aimEffectAngle;
            }
            else
            {
                AimParticles.Stop(true);
            }
        }

        public void StopSmoke()
        {
            if (kartStates.DriftState != Kart.DriftState.Turbo)
            {
                smokeLeftWheel.Stop(true);
                smokeRightWheel.Stop(true);
            }
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

        public void ReloadAbilityParticlesEmit()
        {
            AbilityReloadParticles.Emit(numberOfParticles);
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
