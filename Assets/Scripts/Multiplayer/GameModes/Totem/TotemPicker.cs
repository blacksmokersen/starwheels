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
            _throwPositions = GetComponent<ThrowPositions>();
            _inventory = GetComponent<Inventory>();
        }

        // MONOBEHAVIOUR

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Constants.Tag.Totem))
            {
                Debug.Log("Got it !");
                var totemObject = collision.gameObject;
                totemObject.transform.SetParent(_throwPositions.BackPosition);
                _totem = totemObject.GetComponent<Throwable>();

                _inventory.StopAllCoroutines(); // Stop any anti-spam routine
                _inventory.CanUseItem = false;
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
            if (Input.GetButtonDown(Constants.Input.UseItem))
            {
                UseTotem();
            }
        }

        // PRIVATE

        private void UseTotem()
        {
            _throwableLauncher.Throw(_totem);
            _inventory.CanUseItem = true;
        }
    }
}
