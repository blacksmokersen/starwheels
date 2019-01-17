using UnityEngine;
using Common.PhysicsUtils;

namespace GameModes.Totem
{
    [DisallowMultipleComponent]
    public class TotemOwnerSlower : MonoBehaviour
    {
        [Header("Speed Reduction")]
        [SerializeField] private ClampSpeedSettings _clampSpeedSettings;
        [SerializeField] private ClampSpeed _ownerClampSpeed;

        [Header("Settings")]
        [SerializeField] private TotemSettings _totemSettings;

        private bool _ownerIsSlowed = false;

        public void SlowOwner()
        {
            if (!_ownerIsSlowed)
            {
                var targetMagnitude = _clampSpeedSettings.BaseMaxSpeed * _totemSettings.OwnerSpeedReductionFactor;

                _ownerClampSpeed.SetClampMagnitude(targetMagnitude);
                _ownerIsSlowed = true;
            }
        }

        public void ResetOwnerSpeed()
        {
            if (_ownerIsSlowed)
            {
                _ownerClampSpeed.ResetClampMagnitude();
                _ownerIsSlowed = false;
            }
        }
    }
}
