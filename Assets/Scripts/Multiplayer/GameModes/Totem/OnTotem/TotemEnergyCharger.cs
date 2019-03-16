using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Gamemodes
{
    public class TotemEnergyCharger : MonoBehaviour
    {
        [SerializeField] private Renderer _totemRenderer;

        [Header("Unity Events")]
        public UnityEvent OnFullyDischarged;
        public UnityEvent OnStartChargingEnergy;
        public FloatEvent OnChargingEnergy;
        public UnityEvent OnFullyCharged;

        [Header("Energy Values")]
        [SerializeField] private string _shaderParameterName;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        [Header("Charging Parameter")]
        [SerializeField] private float _chargingDuration;
        [SerializeField] private float _chargingStep;

        [SerializeField] private Material _totemMaterial;
        private float _currentEnergyValue = 0f;
        private Coroutine _chargeCoroutine;

        // CORE

        private void Awake()
        {
            _totemMaterial = _totemRenderer.material;
        }

        // PUBLIC

        public void FullyDischargeTotem()
        {
            StopAllCoroutines();
            UpdateShaderApparition(_minValue);

            if (OnFullyDischarged != null)
            {
                OnFullyDischarged.Invoke();
                Debug.Log("Fully Discharged");
            }
        }

        public void FullyChargeTotem()
        {
            StopAllCoroutines();
            UpdateShaderApparition(_maxValue);

            if (OnFullyCharged != null)
            {
                OnFullyCharged.Invoke();
                Debug.Log("Fully Charged");
            }
        }

        public void StartCharging()
        {
            StopAllCoroutines();
            _chargeCoroutine = StartCoroutine(ChargeTotemRoutine());
        }

        public void UpdateShaderApparition(float val)
        {
            _totemMaterial.SetFloat(Shader.PropertyToID(_shaderParameterName), val);
        }

        // PRIVATE

        private IEnumerator ChargeTotemRoutine()
        {
            yield return new WaitForSeconds(1.5f);
            OnStartChargingEnergy.Invoke();

            OnChargingEnergy.Invoke(0f);
            var currentDuration = 0f;
            while (currentDuration < _chargingDuration)
            {
                _currentEnergyValue = (_maxValue - _minValue) * (currentDuration/_chargingDuration) + _minValue;
                UpdateShaderApparition(_currentEnergyValue);
                yield return new WaitForSeconds(_chargingStep);
                currentDuration += _chargingStep;
                OnChargingEnergy.Invoke(currentDuration/_chargingStep);
            }
            _currentEnergyValue = _maxValue;
            OnFullyCharged.Invoke();
        }
    }
}
