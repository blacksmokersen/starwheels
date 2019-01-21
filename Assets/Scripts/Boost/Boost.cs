using System.Collections;
using UnityEngine;
using Common.PhysicsUtils;
using Engine;

namespace Boost
{
    public class Boost : MonoBehaviour
    {
        [Header("Boost Settings")]
        [SerializeField] private BoostSettings _settings;

        [Header("Boost Dependencies")]
        [SerializeField] private ClampSpeedSettings _clampSpeedSettings;
        [SerializeField] private Rigidbody _rb;

        [Header("KartSpecific Boost Dependencies")]
        [SerializeField] private EngineBehaviour _engine;
        [SerializeField] private BoolVariable _boostModeActivated;
        [SerializeField] private FloatVariable _boostModeValue;

        private Coroutine _physicsBoostCoroutine;
        private Coroutine _directImpulseXBoost;
        private Coroutine _uniqueImpulseXBoostWithXMaxClampSpeed;
        private Coroutine _constantXBoostForXSeconds;
        private Coroutine _xBoostOnAccelerationForXSecondsCoroutine;
        private Coroutine _returnToClampSpeedBaseValue;
        private float _controlClampSpeed;
        private float _currentClampSpeed;
        //  private float _currentTimer;

        // PUBLIC

        public void StartTurbo()
        {
            _controlClampSpeed = _clampSpeedSettings.BaseMaxSpeed;
            //   StartCoroutine(EnterTurbo());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                ConstantXBoostForXSeconds(0.5f, 3, 10, 2);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                XBoostOnAccelerationForXSeconds(500, 3, 10, 1);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                UniqueImpulseXBoostWithXMaxClampSpeed(5000f, 5, 1, 1);
            }
        }

        public void UniqueImpulseXBoostWithXMaxClampSpeed(float boostPower, float maxClampSpeed, float maxClampSpeedDuration, float returnToBaseValueDuration)
        {
            StartCoroutine(UniqueImpulseXBoostWithXMaxClampSpeedCoRoutine(boostPower, maxClampSpeed, maxClampSpeedDuration, returnToBaseValueDuration));
        }

        public void ConstantXBoostForXSeconds(float boostPowerMultiplicator, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {
            StartCoroutine(ConstantXBoostForXSecondsCoroutine(boostPowerMultiplicator, duration, maxClampSpeed, returnToBaseValueDuration));
        }

        public void XBoostOnAccelerationForXSeconds(float boostPower, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {
            StartCoroutine(XBoostOnAccelerationForXSecondsCoroutine(boostPower, duration, maxClampSpeed, returnToBaseValueDuration));
        }

        public void CustomBoostFromBoostSettings(BoostSettings settings)
        {
            if (settings.IsNotStackable)
            {
                if (_uniqueImpulseXBoostWithXMaxClampSpeed != null)
                    StopCoroutine(_uniqueImpulseXBoostWithXMaxClampSpeed);
                if (_xBoostOnAccelerationForXSecondsCoroutine != null)
                    StopCoroutine(_xBoostOnAccelerationForXSecondsCoroutine);
                if (_constantXBoostForXSeconds != null)
                    StopCoroutine(_constantXBoostForXSeconds);

                _clampSpeedSettings.CurrentMaxSpeed = _clampSpeedSettings.BaseMaxSpeed;

                if (settings.HasADirectImpulse)
                    _uniqueImpulseXBoostWithXMaxClampSpeed = StartCoroutine(UniqueImpulseXBoostWithXMaxClampSpeedCoRoutine(settings.DIPower,
                        settings.DIClampSpeedIncrease,
                        settings.DIClampSpeedIncreaseDuration,
                        settings.DIClampSpeedDecreaseDuration));

                if (settings.IsEngineBoostActivated)
                    _xBoostOnAccelerationForXSecondsCoroutine = StartCoroutine(XBoostOnAccelerationForXSecondsCoroutine(settings.EngineBoostValue,
                        settings.BoostDuration,
                        settings.ClampSpeedIncrease,
                        settings.SecondsToDecreaseClampSpeed));

                _constantXBoostForXSeconds = StartCoroutine(ConstantXBoostForXSecondsCoroutine(settings.BoostPercentagMultiplicator,
                    settings.BoostDuration,
                    settings.ClampSpeedIncrease,
                    settings.SecondsToDecreaseClampSpeed));
            }
            else
            {
                if (settings.HasADirectImpulse)
                    StartCoroutine(UniqueImpulseXBoostWithXMaxClampSpeedCoRoutine(settings.DIPower,
                        settings.DIClampSpeedIncrease,
                        settings.DIClampSpeedIncreaseDuration,
                        settings.DIClampSpeedDecreaseDuration));

                if (settings.IsEngineBoostActivated)
                    StartCoroutine(XBoostOnAccelerationForXSecondsCoroutine(settings.EngineBoostValue,
                        settings.BoostDuration,
                        settings.ClampSpeedIncrease,
                        settings.SecondsToDecreaseClampSpeed));

                StartCoroutine(ConstantXBoostForXSecondsCoroutine(settings.BoostPercentagMultiplicator,
                    settings.BoostDuration,
                    settings.ClampSpeedIncrease,
                    settings.SecondsToDecreaseClampSpeed));
            }
        }

        // PRIVATE
        /*
        private IEnumerator EnterTurbo()
        {
            if (_physicsBoostCoroutine != null)
            {
                StopCoroutine(_physicsBoostCoroutine);
            }
            _physicsBoostCoroutine = StartCoroutine(PhysicsBoost(_settings.BoostDuration, _settings.IncreaseMaxSpeedBy));

            yield return new WaitForSeconds(_settings.BoostDuration);
        }
        */
        /*
        private IEnumerator PhysicsBoost(float boostDuration, float magnitudeBoost)
        {
            _clampSpeedSettings.CurrentMaxSpeed = Mathf.Clamp(_clampSpeedSettings.CurrentMaxSpeed, 0, _controlClampSpeed) + magnitudeBoost;

           var _currentTimer = 0f;
            while (_currentTimer < boostDuration)
            {
                _rb.AddRelativeForce(Vector3.forward * _settings.BoostPowerImpulse, ForceMode.VelocityChange);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _currentTimer = 0f;
            while (_currentTimer < boostDuration)
            {
                _clampSpeedSettings.CurrentMaxSpeed = Mathf.Lerp(_controlClampSpeed + magnitudeBoost, _clampSpeedSettings.CurrentMaxSpeed, _currentTimer / boostDuration);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        */

        private IEnumerator UniqueImpulseXBoostWithXMaxClampSpeedCoRoutine(float boostPower, float maxClampSpeed, float maxClampSpeedDuration, float returnToBaseValueDuration)
        {
            _clampSpeedSettings.CurrentMaxSpeed += Mathf.Clamp(_clampSpeedSettings.CurrentMaxSpeed, 0, _controlClampSpeed) + maxClampSpeed;

            var _currentTimer = 0f;
            while (_currentTimer < 0.5f)
            {
                _rb.AddRelativeForce(Vector3.forward * boostPower, ForceMode.Force);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _rb.AddRelativeForce(Vector3.forward * boostPower);
            yield return new WaitForSeconds(maxClampSpeedDuration);
            StartCoroutine(ReturnToClampSpeedBaseValue(maxClampSpeed, returnToBaseValueDuration));
        }

        private IEnumerator ConstantXBoostForXSecondsCoroutine(float boostPercentageMultiplicator, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {
            _clampSpeedSettings.CurrentMaxSpeed += maxClampSpeed;

            var _currentTimer = 0f;
            while (_currentTimer < duration)
            {
                _rb.AddRelativeForce(Vector3.forward * boostPercentageMultiplicator, ForceMode.VelocityChange);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            StartCoroutine(ReturnToClampSpeedBaseValue(maxClampSpeed, returnToBaseValueDuration));
        }

        private IEnumerator XBoostOnAccelerationForXSecondsCoroutine(float boostPower, float duration, float maxClampSpeed, float returnToBaseValueDuration)
        {
            _clampSpeedSettings.CurrentMaxSpeed += Mathf.Clamp(_clampSpeedSettings.CurrentMaxSpeed, 0, _controlClampSpeed) + maxClampSpeed;
            _boostModeActivated.Value = true;
            _boostModeValue.Value = boostPower;
            yield return new WaitForSeconds(duration);
            _boostModeActivated.Value = false;
            _boostModeValue.Value = 0;

            StartCoroutine(ReturnToClampSpeedBaseValue(maxClampSpeed, returnToBaseValueDuration));
        }

        private IEnumerator ReturnToClampSpeedBaseValue(float clampSpeedValueToDecrease, float returnDuration)
        {
            var stepDuration = returnDuration / 100;
            var stepNumber = 100;
            int i = 0;
            while (i < stepNumber)
            {
                _clampSpeedSettings.CurrentMaxSpeed -= clampSpeedValueToDecrease / stepNumber;
                yield return new WaitForSeconds(stepDuration);
                i++;
            }
        }
    }
}
