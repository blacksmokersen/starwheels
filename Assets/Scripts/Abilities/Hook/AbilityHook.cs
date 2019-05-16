using UnityEngine;
using Items;

namespace Abilities
{
    public class AbilityHook : Ability, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [SerializeField] private GameObject prefabHook;
        [SerializeField] private HookSettings hookSettings;

        [Header("Effects")]
        [SerializeField] private ParticleSystem reloadParticlePrefab;
        [SerializeField] private int reloadParticleNumber;

        private GameObject _ownerKart;
        private bool _canUseAbility = true;

        // CORE

        // BOLT

        public override void SimulateController()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled && Input.GetButtonDown(Constants.Input.UseAbility))
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
                /*
                var position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
                var hook = BoltNetwork.Instantiate(prefabHook, position, transform.rotation);
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
