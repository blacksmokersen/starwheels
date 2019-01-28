using UnityEngine;
using Common.PhysicsUtils;

namespace GameModes.Totem
{
    [DisallowMultipleComponent]
    public class TotemOwnerSlower : MonoBehaviour
    {
        [Header("Speed Reduction")]
        [SerializeField] private ClampSpeedSettings _clampSpeedSettings;
        [SerializeField] private Boost _ownerBoost;
        [SerializeField] private ClampSpeed _ownerClampSpeed;

        [Header("Settings")]
        [SerializeField] private TotemSettings _totemSettings;

        private bool _ownerIsSlowed = false;

        public void SlowOwner()
        {
            if (!_ownerIsSlowed)
            {
                _ownerBoost.StopAllCoroutines();
                var targetMagnitude = _clampSpeedSettings.BaseClampSpeed * _totemSettings.OwnerSpeedReductionFactor;

                _ownerClampSpeed.SetClampMagnitude(targetMagnitude);
                _ownerIsSlowed = true;
            }
        }

        public void ResetOwnerSpeed()
        {
            if (_ownerIsSlowed)
            {
                _ownerBoost.StopAllCoroutines();
                _ownerClampSpeed.ResetClampMagnitude();
                _ownerIsSlowed = false;
            }
        }
    }
}
