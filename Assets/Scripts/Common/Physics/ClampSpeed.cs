using System.Collections;
using UnityEngine;

namespace Common.PhysicsUtils
{
    public class ClampSpeed : MonoBehaviour
    {
        [SerializeField] private ClampSpeedSettings _clampSpeedSettings;

        [HideInInspector] public float BaseClampSpeed;
        [HideInInspector] public float CurrentClampSpeed;

        private Rigidbody _rigidbody;

        // CORE

        private void Awake()
        {
            _rigidbody = GetComponentInParent<Rigidbody>();
            BaseClampSpeed = _clampSpeedSettings.BaseClampSpeed;
            CurrentClampSpeed = BaseClampSpeed;
        }

        private void FixedUpdate()
        {
            ClampMagnitude();
        }

        // PUBLIC

        public void SetClampMagnitude(float magnitude)
        {
            CurrentClampSpeed = magnitude;
        }

        public void ClampForXSeconds(float magnitude, float seconds)
        {
            StartCoroutine(ClampForXSecondsRoutine(magnitude, seconds));
        }

        public void ResetClampMagnitude()
        {
            CurrentClampSpeed = BaseClampSpeed;
        }

        // PRIVATE

        private void ClampMagnitude()
        {
            if (CurrentClampSpeed > 0)
            {
                if (_clampSpeedSettings.ClampYAxis)
                {
                    _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, CurrentClampSpeed);
                }
                else
                {
                    var horizontalVelocity = Vector3.ClampMagnitude(new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z), CurrentClampSpeed);
                    _rigidbody.velocity = new Vector3(horizontalVelocity.x, _rigidbody.velocity.y, horizontalVelocity.z);
                }
            }
        }

        private IEnumerator ClampForXSecondsRoutine(float magnitude, float seconds)
        {
            CurrentClampSpeed = magnitude;
            yield return new WaitForSeconds(seconds);
            CurrentClampSpeed = BaseClampSpeed;
        }
    }
}
