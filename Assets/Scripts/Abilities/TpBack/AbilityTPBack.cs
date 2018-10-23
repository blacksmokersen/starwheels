using Items;
using UnityEngine;

namespace Abilities
{

    public class AbilityTPBack : MonoBehaviour, IControllable
    {

        [SerializeField] private GameObject prefabTpBack;
        [SerializeField] private ThrowableLauncher throwableLauncher;

        private TPBackBehaviour _tpBack = null;
        private bool _canUseAbility = true;
        private Rigidbody _rb;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseAbility))
            {
                Use();
            }
        }

        public void Use()
        {
            if (_canUseAbility)
            {
                if (_tpBack == null)
                {
                    var instantiatedItem = BoltNetwork.Instantiate(prefabTpBack);
                    var throwable = instantiatedItem.GetComponent<Throwable>();
                    _tpBack = instantiatedItem.GetComponent<TPBackBehaviour>();
                    throwableLauncher.Throw(throwable);
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
