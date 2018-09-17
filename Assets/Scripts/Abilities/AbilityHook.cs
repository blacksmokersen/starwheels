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
            var hook = Instantiate(_hookPrefab, transform.position, transform.rotation);
            hook.GetComponent<HookBehaviour>().KartInventory = _kartInventory;
        }
    }
}
