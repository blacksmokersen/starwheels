using System.Collections;
using UnityEngine;

namespace Common.PhysicsUtils
{
    public class ClampSpeed : MonoBehaviour
    {
        [SerializeField] private ClampSpeedSettings _clampSpeedSettings;

        private Rigidbody _rigidbody;

        // CORE

        private void Awake()
        {
            _rigidbody = GetComponentInParent<Rigidbody>();
            _clampSpeedSettings.CurrentMaxSpeed = _clampSpeedSettings.BaseMaxSpeed;
        }

        private void FixedUpdate()
        {
            ClampMagnitude();
        }

        // PUBLIC

        public void SetClampMagnitude(float magnitude)
        {
            _clampSpeedSettings.CurrentMaxSpeed = magnitude;
        }

        public void ClampForXSeconds(float magnitude, float seconds)
        {
            StartCoroutine(ClampForXSecondsRoutine(magnitude, seconds));
        }

        public void ResetClampMagnitude()
        {
            _clampSpeedSettings.CurrentMaxSpeed = _clampSpeedSettings.BaseMaxSpeed;
        }

        // PRIVATE

        private void ClampMagnitude()
        {
            if (_clampSpeedSettings.CurrentMaxSpeed > 0)
            {
                _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _clampSpeedSettings.CurrentMaxSpeed);
            }
        }

        private IEnumerator ClampForXSecondsRoutine(float magnitude, float seconds)
        {
            _clampSpeedSettings.CurrentMaxSpeed = magnitude;
            yield return new WaitForSeconds(seconds);
            _clampSpeedSettings.CurrentMaxSpeed = _clampSpeedSettings.BaseMaxSpeed;
        }
    }
}
