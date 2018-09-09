using UnityEngine;
using System.Collections;

namespace Audio
{
    public class KartSoundsScript : BaseKartComponent
    {
        // Clips
        [Header("Engine")]
        [SerializeField] private AudioClip MotorAccelClip;
        [SerializeField] private AudioClip MotorFullClip;
        [SerializeField] private AudioClip MotorDecelClip;

        [Header("Drift")]
        [SerializeField] private AudioClip DriftStartClip;
        [SerializeField] private AudioClip DriftFullClip;
        [SerializeField] private AudioClip DriftEndClip;
        [SerializeField] private AudioClip BoostClip;

        [Header("Jump")]
        [SerializeField] private AudioClip FirstJumpClip;
        [SerializeField] private AudioClip SecondJumpClip;

        [Header("Items")]
        [SerializeField] private AudioClip PlayerHitClip;
        [SerializeField] private AudioClip ItemBoxClip;
        [SerializeField] private AudioClip ItemLotteryClip;

        // Audio sources
        private AudioSource _soundManager;
        private AudioSource _motorSource;
        private AudioSource _driftSource;
        private AudioSource _lotterySource;
        private float _pitchMotorMagnitudeDiviser = 27f;
        private Coroutine _delayDriftStartRoutine;

        // CORE

        private new void Awake()
        {
            base.Awake();

            _soundManager = gameObject.AddComponent<AudioSource>();
            _soundManager.spatialBlend = 1f;
            _soundManager.volume = 1;

            _motorSource = gameObject.AddComponent<AudioSource>();
            _motorSource.spatialBlend = 1f;
            _motorSource.volume = 1;
            _motorSource.loop = true;
            _motorSource.clip = MotorFullClip;

            _driftSource = gameObject.AddComponent<AudioSource>();
            _driftSource.spatialBlend = 1f;
            _driftSource.volume = 1;
            _driftSource.loop = true;
            _driftSource.clip = DriftFullClip;

            _lotterySource = gameObject.AddComponent<AudioSource>();
            _lotterySource.spatialBlend = 1f;
            _lotterySource.volume = 1;
            _lotterySource.loop = true;
            _lotterySource.clip = ItemLotteryClip;

            PlayMotorFullSound();

            kartEvents.OnJump += PlayFirstJumpSound;
            kartEvents.OnDoubleJump += (a) => PlaySecondJumpSound();

            kartEvents.OnDriftBoostStart += PlayBoostSound;
            kartEvents.OnDriftBoostStart += PlayDriftEndSound;
            kartEvents.OnDriftStart += PlayDriftStartSound;
            kartEvents.OnDriftEnd += PlayDriftEndSound;

            kartEvents.OnVelocityChange += (velocity) =>
            {
                Vector3 newVelocity = new Vector3(velocity.x, 0, velocity.z);
                SetMotorPitch(0.5f + 0.35f * newVelocity.magnitude / _pitchMotorMagnitudeDiviser);
            };

            kartEvents.OnHit += PlayPlayerHitSound;

            kartEvents.OnItemBoxGet += StartItemLotterySound;
            kartEvents.OnLotteryStop += StopItemLotterySound;
            kartEvents.OnLotteryStop += PlayItemBoxSound;
        }

        // PUBLIC

        // PRIVATE

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

        private void StartItemLotterySound()
        {
            _lotterySource.Play();
        }

        private void StopItemLotterySound()
        {
            _lotterySource.Stop();
        }
        #endregion
    }
}
