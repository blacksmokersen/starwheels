using UnityEngine;
using Items;

namespace Abilities
{
    public class AbilityHook : Ability
    {
        [SerializeField] private GameObject hookPrefab;
        [SerializeField] private KartInventory kartInventory;

        private GameObject _ownerKart;

        // CORE



        // PUBLIC

        public void Throw()
        {
            var hook = Instantiate(hookPrefab, transform.position, Quaternion.identity);
            hook.GetComponent<HookBehaviour>().KartInventory = kartInventory;
        }
    }
}
