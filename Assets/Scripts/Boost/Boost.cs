using System.Collections;
using UnityEngine;
using Common.PhysicsUtils;
using Engine;

namespace Common.PhysicsUtils
{
    public class Boost : MonoBehaviour
    {
        [Header("Boost Settings")]
        [SerializeField] private BoostSettings _settings;

        [Header("Boost Dependencies")]
        //  [SerializeField] private ClampSpeedSettings _clampSpeedSettings;
        [SerializeField] private ClampSpeed _clampSpeed;
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

        // PUBLIC

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

        public void StopAllCurrentCustomBoost(BoostSettings settings)
        {
            StopAllCoroutines();
            _clampSpeed.CurrentClampSpeed = _clampSpeed.BaseClampSpeed;
        }

        //PRIVATE

        private IEnumerator UniqueImpulseXBoostWithXMaxClampSpeedCoRoutine(float boostPower, float maxClampSpeed, float maxClampSpeedDuration, float returnToBaseValueDuration)
        {
            _clampSpeed.CurrentClampSpeed += maxClampSpeed;

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
            _clampSpeed.CurrentClampSpeed += maxClampSpeed;

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
            _clampSpeed.CurrentClampSpeed += maxClampSpeed;
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
                _clampSpeed.CurrentClampSpeed -= clampSpeedValueToDecrease / stepNumber;
                yield return new WaitForSeconds(stepDuration);
                i++;
            }
        }
    }
}
