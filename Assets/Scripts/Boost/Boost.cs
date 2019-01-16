using System.Collections;
using UnityEngine;
using Common.PhysicsUtils;

namespace Boost
{
    public class Boost : MonoBehaviour
    {
        [Header("Boost Settings")]
        [SerializeField] private BoostSettings _settings;

        [Header("Boost Dependencies")]
        [SerializeField] private ClampSpeed _clampSpeed;
        [SerializeField] private Rigidbody _rb;

        private Coroutine _physicsBoostCoroutine;
        private float _controlMagnitude;
        private float _currentTimer;

        // PUBLIC

        public void StartTurbo()
        {
            _controlMagnitude = _clampSpeed.ControlMaxSpeed;
            StartCoroutine(EnterTurbo());
        }

        public void StartTurbo(BoostSettings settings)
        {

        }

        // PRIVATE

        private IEnumerator EnterTurbo()
        {
            if (_physicsBoostCoroutine != null)
            {
                StopCoroutine(_physicsBoostCoroutine);
            }
            _physicsBoostCoroutine = StartCoroutine(PhysicsBoost(_settings.BoostDuration, _settings.IncreaseMaxSpeedBy));

            yield return new WaitForSeconds(_settings.BoostDuration);
        }

        private IEnumerator PhysicsBoost(float boostDuration, float magnitudeBoost)
        {
            _clampSpeed.CurrentMaxSpeed = Mathf.Clamp(_clampSpeed.CurrentMaxSpeed, 0, _controlMagnitude) + magnitudeBoost;

            _currentTimer = 0f;
            while (_currentTimer < boostDuration)
            {
                _rb.AddRelativeForce(Vector3.forward * _settings.BoostPowerImpulse, ForceMode.VelocityChange);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _currentTimer = 0f;
            while (_currentTimer < boostDuration)
            {
                _clampSpeed.CurrentMaxSpeed = Mathf.Lerp(_controlMagnitude + magnitudeBoost, _clampSpeed.ControlMaxSpeed, _currentTimer / boostDuration);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
