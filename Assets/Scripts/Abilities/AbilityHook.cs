using UnityEngine;
using Items;
using Photon.Pun;

namespace Abilities
{
    public class AbilityHook : Ability
    {
        private KartInventory _kartInventory;
        private GameObject _ownerKart;

        // CORE

        private new void Awake()
        {
            base.Awake();
            _kartInventory = GetComponentInParent<Kart.KartHub>().kartInventory;
        }

        // PUBLIC

        public override void Use(float xAxis, float yAxis)
        {
            var position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
            var hook = PhotonNetwork.Instantiate(Constants.Prefab.HookObject, position, transform.rotation);
            var hookBehaviour = hook.GetComponent<HookBehaviour>();
            hookBehaviour.OwnerKartInventory = _kartInventory;
            hookBehaviour.SetOwner(_kartInventory.transform);
        }
    }
}
