using UnityEngine;
using System.Collections;

namespace Audio
{
    public class KartSoundsScript : BaseKartComponent
    {
        // Clips
        [Header("Engine")]
        public AudioClip MotorAccelClip;
        public AudioClip MotorFullClip;
        public AudioClip MotorDecelClip;

        [Header("Drift")]
        public AudioClip DriftStartClip;
        public AudioClip DriftFullClip;
        public AudioClip DriftEndClip;
        public AudioClip BoostClip;

        [Header("Jump")]
        public AudioClip FirstJumpClip;
        public AudioClip SecondJumpClip;

        [Header("Items")]
        public AudioClip PlayerHitClip;
        public AudioClip ItemBoxClip;
        public AudioClip ItemLotteryClip;

        // Audio sources
        private AudioSource _soundManager;
        private AudioSource _motorSource;
        private AudioSource _driftSource;
        private float _pitchMotorMagnitudeDiviser = 27f;
        private Coroutine _delayDriftStartRoutine;

        private new void Awake()
        {
            base.Awake();
            _soundManager = gameObject.AddComponent<AudioSource>();
            _soundManager.spatialBlend = 1f;

            _motorSource = gameObject.AddComponent<AudioSource>();
            _motorSource.spatialBlend = 1f;
            _motorSource.loop = true;
            _motorSource.clip = MotorFullClip;

            _driftSource = gameObject.AddComponent<AudioSource>();
            _driftSource.spatialBlend = 1f;
            _driftSource.loop = true;
            _driftSource.clip = DriftFullClip;

            PlayMotorFullSound();

            kartEvents.OnJump += PlayFirstJumpSound;
            kartEvents.OnDriftBoost += PlayBoostSound;
            kartEvents.OnDriftBoost += PlayDriftEndSound;
            kartEvents.OnDriftStart += PlayDriftStartSound;
            kartEvents.OnDriftEnd += PlayDriftEndSound;
            kartEvents.OnDoubleJump += (a) => PlaySecondJumpSound();
            kartEvents.OnVelocityChange += (velocity) =>
            {
                Vector3 newVelocity = new Vector3(velocity.x, 0, velocity.z);
                SetMotorPitch(0.5f + 0.35f * newVelocity.magnitude / _pitchMotorMagnitudeDiviser);
            };
            kartEvents.OnHit += PlayPlayerHitSound;
            kartEvents.OnCollisionEnterItemBox += PlayItemBoxSound;
            kartEvents.OnCollisionEnterItemBox += PlayItemLotterySound;
        }

        #region Engine
        private void PlayMotorAccelSound()
        {
            if (!_soundManager.isPlaying)
            {
                _soundManager.PlayOneShot(MotorAccelClip);
            }
        }

        private void SetMotorPitch(float pitch)
        {
            _motorSource.pitch = pitch;
        }

        private void PlayMotorFullSound()
        {
            _motorSource.Play();
        }

        private void PlayMotorDecelSound()
        {
            if (!_soundManager.isPlaying)
            {
                _soundManager.PlayOneShot(MotorDecelClip);
            }
        }
        #endregion

        #region Drift
        private void StartDriftSource()
        {
            // AudioSource.PlayClipAtPoint(DriftStart, transform.position);
            _driftSource.Play();
        }

        private void StopDriftSource()
        {
            _driftSource.Stop();
            // AudioSource.PlayClipAtPoint(DriftEnd, transform.position);
        }

        private void PlayDriftStartSound()
        {
            _driftSource.PlayOneShot(DriftStartClip);
            if (_delayDriftStartRoutine != null) StopCoroutine(_delayDriftStartRoutine);
            _delayDriftStartRoutine = StartCoroutine(DelayDriftStart());
        }

        private void PlayDriftLoopSound()
        {
            _driftSource.clip = DriftFullClip;
            _driftSource.loop = true;
            _driftSource.Play();
        }

        private void PlayDriftEndSound()
        {
            if (_delayDriftStartRoutine != null) StopCoroutine(_delayDriftStartRoutine);
            _driftSource.Stop();
            _driftSource.PlayOneShot(DriftEndClip);
        }

        private void PlayBoostSound()
        {
            _soundManager.PlayOneShot(BoostClip);
        }

        private IEnumerator DelayDriftStart()
        {
            yield return new WaitForSeconds(DriftStartClip.length);
            PlayDriftLoopSound();
        }
        #endregion

        #region Jump
        private void PlayFirstJumpSound()
        {
            _soundManager.PlayOneShot(FirstJumpClip);
        }

        private void PlaySecondJumpSound()
        {
            _soundManager.PlayOneShot(SecondJumpClip);
        }
        #endregion

        #region Items
        private void PlayPlayerHitSound()
        {
            _soundManager.PlayOneShot(PlayerHitClip);
        }

        private void PlayItemBoxSound()
        {
            _soundManager.PlayOneShot(ItemBoxClip);
        }

        private void PlayItemLotterySound()
        {
            _soundManager.PlayOneShot(ItemLotteryClip);
        }
        #endregion
    }
}
