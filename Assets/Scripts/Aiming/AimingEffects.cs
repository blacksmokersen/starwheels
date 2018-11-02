using UnityEngine;

namespace Aiming
{
    public class AimingEffects : MonoBehaviour
    {
        [Header("Particles")]
        public float AimEffectMaxAngle = 45;
        [SerializeField] private ParticleSystem aimParticles;
        [SerializeField] private int numberOfParticles = 300;

        // CORE

        private void Awake()
        {
            aimParticles.Stop(true);
        }

        // PUBLIC

        public void AimEffects(float aimAxisH, float aimAxisV)
        {
            if (Mathf.Abs(aimAxisV) > 0.1)
            {
                aimParticles.Play(true);
                aimParticles.transform.localEulerAngles = new Vector3(0, aimAxisH, 0) * AimEffectMaxAngle;
            }
            else
            {
                aimParticles.Stop(true);
            }
        }
    }
}
