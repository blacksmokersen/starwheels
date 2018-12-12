using UnityEngine;

namespace GameModes.Totem
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class TotemRadiusSetup : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TotemSettings _totemSettings;

        [Header("Totem Pickup Zone")]
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private Transform _meshTransform;

        private void Start()
        {
            _sphereCollider.radius *= _totemSettings.Radius;
            _meshTransform.localScale *= _totemSettings.Radius;
        }
    }
}
