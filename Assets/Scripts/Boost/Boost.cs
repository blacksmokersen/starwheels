using System.Collections;
using UnityEngine;

namespace Boost
{
    public class Boost : MonoBehaviour
    {
        public BoostSettings Settings;

        private Rigidbody _rigidBody;
        private Coroutine _turboCoroutine;
        private Coroutine _physicsBoostCoroutine;

        private void Awake()
        {
            _rigidBody = GetComponentInParent<Rigidbody>();

            //   OnDriftBoostStart += () => DriftTurnState = TurnState.NotTurning;
            //   OnDriftBoostStart += () => DriftState = DriftState.Turbo;

            Settings._controlMagnitude = Settings.MaxMagnitude;
            Settings._controlSpeed = Settings.Speed;

        }

        public void StartTurbo()
        {
            _turboCoroutine = StartCoroutine(EnterTurbo());
        }

        private IEnumerator EnterTurbo()
        {
            if (_physicsBoostCoroutine != null)
            {
                StopCoroutine(_physicsBoostCoroutine);
            }
            _physicsBoostCoroutine = StartCoroutine(BoostPhysic(Settings.BoostDuration, Settings.MagnitudeBoost, Settings.BoostSpeed));

            yield return new WaitForSeconds(Settings.BoostDuration);
        }

        public IEnumerator BoostPhysic(float boostDuration, float magnitudeBoost, float speedBoost)
        {
            Settings.MaxMagnitude = Mathf.Clamp(Settings.MaxMagnitude, 0, Settings._controlMagnitude) + magnitudeBoost;
            Settings.Speed = Mathf.Clamp(Settings.Speed, 0, Settings._controlSpeed) + speedBoost;

            Settings._currentTimer = 0f;
            while (Settings._currentTimer < boostDuration)
            {
                _rigidBody.AddRelativeForce(Vector3.forward * Settings.BoostPowerImpulse, ForceMode.VelocityChange);
                Settings._currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Settings._currentTimer = 0f;
            while (Settings._currentTimer < boostDuration)
            {
                Settings.MaxMagnitude = Mathf.Lerp(Settings._controlMagnitude + magnitudeBoost, Settings._controlMagnitude, Settings._currentTimer / boostDuration);
                Settings.Speed = Mathf.Lerp(Settings._controlSpeed + speedBoost, Settings._controlSpeed, Settings._currentTimer / boostDuration);
                Settings._currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
