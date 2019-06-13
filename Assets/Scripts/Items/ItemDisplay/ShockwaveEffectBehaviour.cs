using UnityEngine;
using Bolt;
using ThrowingSystem;

namespace Items
{
    public class ShockwaveEffectBehaviour : EntityEventListener
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _smallEffectPrefab;
        [SerializeField] private GameObject _mediumEffectPrefab;
        [SerializeField] private GameObject _bigEffectPrefab;

        //BOLT

        public override void OnEvent(ObjectThrow evnt)
        {
            if (entity == evnt.Entity)
            {
                var size = (Size) evnt.Size;
                InstantiateShockwave(evnt.Position, evnt.Rotation, size);
            }
        }

        //PUBLIC

        public void InstantiateShockwave(Vector3 position, Quaternion rotation, Size size)
        {
            GameObject prefab = _smallEffectPrefab;
            switch (size)
            {
                case Size.Small:
                    prefab = _smallEffectPrefab;
                    break;
                case Size.Medium:
                    prefab = _mediumEffectPrefab;
                    break;
                case Size.Big:
                    prefab = _bigEffectPrefab;
                    break;
            }
            var shockwave = Instantiate(prefab, position, rotation, transform);
            Destroy(shockwave, .3f);
        }
    }
}

