using UnityEngine;
using System.Collections;

namespace Audio
{
    public class KartSoundsScript : BaseKartComponent
    {
        [Header("Engine")]
        public AudioSource MotorAccelSource;
        public AudioSource MotorFullSource;
        public AudioSource MotorDecelSource;

        [Header("Drift")]
        public AudioSource DriftStartSource;
        public AudioSource DriftFullSource;
        public AudioSource DriftEndSource;
        public AudioSource BoostSource;

        [Header("Jump")]
        public AudioSource FirstJumpSource;
        public AudioSource SecondJumpSource;

        [Header("Items")]
        public AudioSource PlayerHitSource;
        public AudioSource ItemBoxSource;
        public AudioSource ItemLotterySource;

        private float _pitchMotorMagnitudeDiviser = 27f;
        private Coroutine _delayDriftStartRoutine;

        private new void Awake()
        {
            base.Awake();

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
                SetMotorFullPitch(0.5f + 0.35f * newVelocity.magnitude / _pitchMotorMagnitudeDiviser);
            };
            kartEvents.OnHit += PlayPlayerHitSound;
            kartEvents.OnCollisionEnterItemBox += PlayItemBoxSound;
            kartEvents.OnCollisionEnterItemBox += PlayItemLotterySound;

            if (photonView.isMine)
            {
                gameObject.AddComponent<AudioListener>();
            }
        }

        #region Engine

        private void SetMotorFullPitch(float pitch)
        {
            MotorFullSource.pitch = pitch;
        }

        private void PlayMotorFullSound()
        {
            MotorFullSource.Play();
        }
        #endregion

        #region Drift
        private void StartDriftSource()
        {
            DriftFullSource.Play();
        }

        private void StopDriftSource()
        {
            DriftFullSource.Stop();
        }

        private void PlayDriftStartSound()
        {
            DriftStartSource.Play();
            if (_delayDriftStartRoutine != null)
                StopCoroutine(_delayDriftStartRoutine);
            _delayDriftStartRoutine = StartCoroutine(DelayDriftStart());
        }

        private void PlayDriftLoopSound()
        {
            DriftFullSource.loop = true;
            DriftFullSource.Play();
        }

        private void PlayDriftEndSound()
        {
            if (_delayDriftStartRoutine != null)
                StopCoroutine(_delayDriftStartRoutine);
            DriftFullSource.Stop();
            DriftEndSource.Play();
        }

        private void PlayBoostSound()
        {
            BoostSource.Play();
        }

        private IEnumerator DelayDriftStart()
        {
            yield return new WaitForSeconds(DriftStartSource.clip.length);
            PlayDriftLoopSound();
        }
        #endregion

        #region Jump
        private void PlayFirstJumpSound()
        {
            FirstJumpSource.Play();
        }

        private void PlaySecondJumpSound()
        {
            SecondJumpSource.Play();
        }
        #endregion

        #region Items
        private void PlayPlayerHitSound()
        {
            PlayerHitSource.Play();
        }

        private void PlayItemBoxSound()
        {
            ItemBoxSource.Play();
        }

        private void PlayItemLotterySound()
        {
            ItemLotterySource.Play();
        }
        #endregion
    }
}
