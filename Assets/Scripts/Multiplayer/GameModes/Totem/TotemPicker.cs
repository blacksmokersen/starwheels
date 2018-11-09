using UnityEngine;
using Bolt;
using ThrowingSystem;
using Items;

namespace GameModes.Totem
{
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(ThrowableLauncher))]
    [RequireComponent(typeof(ThrowPositions))]
    public class TotemPicker : EntityBehaviour , IControllable
    {
        private Inventory _inventory;
        private Throwable _totem;
        private ThrowableLauncher _throwableLauncher;
        private ThrowPositions _throwPositions;

        // CORE

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _throwableLauncher = GetComponent<ThrowableLauncher>();
            _throwPositions = GetComponent<ThrowPositions>();
        }

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.Totem) && !_totem && other.isTrigger)
            {
                Debug.Log("Totem : " + other.gameObject.name);
                SetTotem(other.gameObject);
            }
        }

        // BOLT

        public override void SimulateController()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseItem) && _totem)
            {
                UseTotem();
            }
        }

        // PRIVATE

        private void SetTotem(GameObject totem)
        {
            _totem = totem.GetComponentInParent<Throwable>();
            _totem.GetComponent<Rigidbody>().isKinematic = true;
            _totem.GetComponent<SphereCollider>().enabled = false;
            _totem.transform.SetParent(_throwPositions.BackPosition);
            _totem.transform.position = _throwPositions.BackPosition.position;

            _inventory.StopAllCoroutines(); // Stop any anti-spam routine
            _inventory.CanUseItem = false;
        }

        private void UseTotem()
        {
            _totem.GetComponent<Rigidbody>().isKinematic = false;
            _totem.GetComponent<SphereCollider>().enabled = true;
            _totem.GetComponent<TotemBehaviour>().StartSlowdown();
            _totem.transform.SetParent(null);
            _throwableLauncher.Throw(_totem);
            _totem = null;

            _inventory.CanUseItem = true;
        }
    }
}
