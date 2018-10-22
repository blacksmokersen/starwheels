using UnityEngine;
using Items;

namespace Abilities
{
    public class AbilityHook : MonoBehaviour, IControllable
    {
        private GameObject _ownerKart;
        private bool _canUseAbility = true;

        // CORE

        // PUBLIC

        public void MapInputs()
        {
            throw new System.NotImplementedException();
        }

        public void Use(float xAxis, float yAxis)
        {
            if (_canUseAbility)
            {
                /*
                var position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
                var hook = PhotonNetwork.Instantiate(Constants.Prefab.HookObject, position, transform.rotation);
                var hookBehaviour = hook.GetComponent<HookBehaviour>();
                hookBehaviour.OwnerKartInventory = _kartInventory;
                hookBehaviour.SetOwner(_kartInventory.transform);
                StartCoroutine(AbilityCooldown());
                */
            }
        }
    }
}
