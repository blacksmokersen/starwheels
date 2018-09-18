using UnityEngine;
using Items;

namespace Abilities
{
    public class AbilityHook : Ability
    {
        private KartInventory _kartInventory;
        private GameObject _hookPrefab;
        private GameObject _ownerKart;

        // CORE

        private new void Awake()
        {
            base.Awake();
            _kartInventory = GetComponentInParent<Kart.KartHub>().kartInventory;
            _hookPrefab = Resources.Load<GameObject>(Constants.Prefab.HookObject);
        }

        // PUBLIC

        public override void Use(float xAxis, float yAxis)
        {
            var position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
            var hook = Instantiate(_hookPrefab, position, transform.rotation);
            var hookBehaviour = hook.GetComponent<HookBehaviour>();
            hookBehaviour.OwnerKartInventory = _kartInventory;
            hookBehaviour.SetOwner(_kartInventory.transform);
        }
    }
}
