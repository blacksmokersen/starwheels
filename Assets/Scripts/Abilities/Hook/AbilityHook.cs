using UnityEngine;
using Items;

namespace Abilities
{
    public class AbilityHook : AbilitiesBehaviour, IControllable
    {
        [SerializeField] private GameObject prefabHook;
        [SerializeField] private HookSettings hookSettings;
        private GameObject _ownerKart;
        private bool _canUseAbility = true;

        // CORE

        // BOLT

        public override void SimulateController()
        {
            if(abilitiesBehaviourSettings.ActiveAbility == "Hook")
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            //   throw new System.NotImplementedException();
            if (Input.GetButtonDown(Constants.Input.UseAbility))
            {
                var xAxis = Input.GetAxis(Constants.Input.TurnAxis);
                var yAxis = Input.GetAxis(Constants.Input.UpAndDownAxis);
                UseAbility(xAxis,yAxis);
            }
        }

        public void UseAbility(float xAxis, float yAxis)
        {
            if (_canUseAbility)
            {
                var position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
                var hook = BoltNetwork.Instantiate(prefabHook, position, transform.rotation);
                //  var hook = PhotonNetwork.Instantiate(Constants.Prefab.HookObject, position, transform.rotation);
                var hookBehaviour = hook.GetComponent<HookBehaviour>();
              //   hookBehaviour.OwnerKartInventory = _kartInventory;
              //   hookBehaviour.SetOwner(_kartInventory.transform);
                // StartCoroutine(AbilityCooldown());

            }
        }
    }
}
