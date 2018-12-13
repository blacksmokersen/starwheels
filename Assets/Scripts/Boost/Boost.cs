using System.Collections;
using UnityEngine;
using Common.PhysicsUtils;

namespace Boost
{
    public class Boost : MonoBehaviour
    {
        public BoostSettings Settings;

        [SerializeField] private ClampSpeed clampSpeed;

        private Rigidbody _rigidBody;
        private Coroutine _physicsBoostCoroutine;

        private float _controlMagnitude;
        private float _currentTimer;

        private void Awake()
        {
            _rigidBody = GetComponentInParent<Rigidbody>();
        }

        public void StartTurbo()
        {
            _controlMagnitude = clampSpeed.ControlMaxSpeed;
            StartCoroutine(EnterTurbo());
        }

        private IEnumerator EnterTurbo()
        {
            if (_physicsBoostCoroutine != null)
            {
                StopCoroutine(_physicsBoostCoroutine);
            }
            _physicsBoostCoroutine = StartCoroutine(PhysicsBoost(Settings.BoostDuration, Settings.IncreaseMaxSpeedBy, Settings.BoostSpeed));

            yield return new WaitForSeconds(Settings.BoostDuration);
        }

        public IEnumerator PhysicsBoost(float boostDuration, float magnitudeBoost, float speedBoost)
        {
            clampSpeed.CurrentMaxSpeed = Mathf.Clamp(clampSpeed.CurrentMaxSpeed, 0, _controlMagnitude) + magnitudeBoost;

            _currentTimer = 0f;
            while (_currentTimer < boostDuration)
            {
                _rigidBody.AddRelativeForce(Vector3.forward * Settings.BoostPowerImpulse, ForceMode.VelocityChange);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _currentTimer = 0f;
            while (_currentTimer < boostDuration)
            {
                clampSpeed.CurrentMaxSpeed = Mathf.Lerp(_controlMagnitude + magnitudeBoost, clampSpeed.ControlMaxSpeed, _currentTimer / boostDuration);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
