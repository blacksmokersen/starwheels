using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GameModes
{
    public class TotemEnergyCharger : MonoBehaviour
    {
        [SerializeField] private Renderer _totemRenderer;

        [Header("Unity Events")]
        public UnityEvent OnFullyDischarged;
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
            if (_chargeCoroutine != null)
            {
                StopCoroutine(_chargeCoroutine);
            }
            UpdateShaderApparition(_minValue);

            if (OnFullyDischarged != null)
            {
                OnFullyDischarged.Invoke();
            }
        }

        public void FullyChargeTotem()
        {
            if (_chargeCoroutine != null)
            {
                StopCoroutine(_chargeCoroutine);
            }
            UpdateShaderApparition(_maxValue);
            if (OnFullyCharged != null)
            {
                OnFullyCharged.Invoke();
            }
        }
        
        public void StartCharging()
        {
            _chargeCoroutine = StartCoroutine(ChargeTotemRoutine());
        }

        public void UpdateShaderApparition(float val)
        {
            _totemMaterial.SetFloat(Shader.PropertyToID(_shaderParameterName), val);
        }

        // PRIVATE

        private IEnumerator ChargeTotemRoutine()
        {
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
        }
    }
}