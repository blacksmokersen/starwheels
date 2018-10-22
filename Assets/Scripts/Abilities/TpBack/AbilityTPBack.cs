using Items;
using UnityEngine;

namespace Abilities
{
    [RequireComponent(typeof(ThrowableLauncher))]
    public class AbilityTPBack : MonoBehaviour, IControllable
    {
        [SerializeField] private GameObject prefabTpBack;
        private TPBackBehaviour _tpBack = null;
        private bool _canUseAbility = true;

        private ThrowableLauncher _projectileLauncher;
        private Rigidbody _rb;

        // CORE

        private void Awake()
        {
            _projectileLauncher = GetComponent<ThrowableLauncher>();
            _rb = GetComponentInParent<Rigidbody>();
        }

        // PUBLIC

        private void FixedUpdate()
        {
            MapInputs();
        }

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseAbility))
            {
                UseItem();
            }
        }

        public void UseItem()
        {
            var instantiatedItem = BoltNetwork.Instantiate(prefabTpBack);
            var throwable = instantiatedItem.GetComponent<Throwable>();
            _projectileLauncher.Throw(throwable);
        }


        public void Use(float xAxis, float yAxis)
        {
            if (_canUseAbility)
            {
                if (_tpBack == null)
                {
                    /*
                    Direction direction = Direction.Forward;
                    if (yAxis < 0) direction = Direction.Backward;

                    _tpBack = ((GameObject)Instantiate(Resources.Load("Items/TPBack"))).GetComponent<TPBackBehaviour>();
                    _tpBack.Launch(kartHub.kartInventory, direction);
                    */
                    var instantiatedItem = BoltNetwork.Instantiate(prefabTpBack);
                    var throwable = instantiatedItem.GetComponent<Throwable>();
                    _projectileLauncher.Throw(throwable);
                }
                else //if (_tpBack.IsEnabled())
                {
                    _rb.transform.position = _tpBack.transform.position;
                    _rb.transform.rotation = _tpBack.GetKartRotation();
                    Destroy(_tpBack.gameObject);
                    //StartCoroutine(AbilityCooldown());
                }
            }
        }

        // PRIVATE

    }
}
