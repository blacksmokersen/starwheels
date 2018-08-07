using UnityEngine;

namespace Audio
{
    public class KartSoundsScript : BaseKartComponent
    {
        // Clips
        public AudioClip MotorAccel;
        public AudioClip Motor;
        public AudioClip MotorDecel;
        public AudioClip DriftStart;
        public AudioClip Drift;
        public AudioClip DriftEnd;
        public AudioClip FirstJump;
        public AudioClip SecondJump;
        public AudioClip PlayerHit;
        public AudioClip Boost;

        // Audio sources
        private AudioSource soundManager;
        private AudioSource motorSource;
        private AudioSource driftSource;
        private float pitchMotorMagnitudeDiviser = 27;

        private new void Awake()
        {
            base.Awake();
            kartEvents.OnHit += Playerhit;
            soundManager = gameObject.AddComponent<AudioSource>();
            soundManager.spatialBlend = 1f;

            motorSource = gameObject.AddComponent<AudioSource>();
            motorSource.spatialBlend = 1f;
            motorSource.loop = true;
            motorSource.clip = Motor;

            driftSource = gameObject.AddComponent<AudioSource>();
            driftSource.spatialBlend = 1f;
            driftSource.loop = true;
            driftSource.clip = Drift;

            PlayMotor();

            kartEvents.OnJump += PlayFirstJump;
            kartEvents.OnDoubleJump += (a) => PlaySecondJump();
            kartEvents.OnVelocityChange += (magnitude) => SetMotorPitch(0.5f + 0.5f * magnitude/pitchMotorMagnitudeDiviser);//(localVelocity.magnitude / MaxMagnitude));
        }

        public void PlayMotorAccel()
        {
            if (!soundManager.isPlaying)
            {
                soundManager.PlayOneShot(MotorAccel);
            }
        }

        public void SetMotorPitch(float pitch)
        {
            motorSource.pitch = pitch;
        }

        public void PlayMotor()
        {
            motorSource.Play();
        }
        public void PlayMotorDecel()
        {
            if (!soundManager.isPlaying)
            {
                soundManager.PlayOneShot(MotorDecel);
            }
        }

        public void StartDrift()
        {
            // AudioSource.PlayClipAtPoint(DriftStart, transform.position);
            driftSource.Play();
        }
        public void EndDrift()
        {
            driftSource.Stop();
            // AudioSource.PlayClipAtPoint(DriftEnd, transform.position);
        }

        public void PlayDriftStart()
        {
            soundManager.PlayOneShot(DriftStart);
        }
        public void PlayDrift()
        {
                soundManager.clip = Drift;
                soundManager.Play();
        }
        public void PlayDriftEnd()
        {
            soundManager.PlayOneShot(DriftEnd);
        }

        public void PlayFirstJump()
        {
            soundManager.PlayOneShot(FirstJump);
        }
        public void PlaySecondJump()
        {
            soundManager.PlayOneShot(SecondJump);
        }
        public void Playerhit()
        {
            soundManager.PlayOneShot(PlayerHit);
        }
        public void BoostSound()
        {
            soundManager.PlayOneShot(Boost);
        }
    }
}
