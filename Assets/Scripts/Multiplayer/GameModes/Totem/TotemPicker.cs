using UnityEngine;
using Bolt;
using ThrowingSystem;
using Items;

namespace GameModes.Totem
{
    public class TotemPicker : EntityBehaviour<IKartState> , IControllable
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private ThrowPositions _throwPositions;

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.isServer && other.CompareTag(Constants.Tag.Totem) && other.isTrigger) // Server sees a player collide with totem trigger
            {
                var totemBehaviour = other.GetComponentInParent<TotemBehaviour>();
                if (totemBehaviour.CanBePickedUp)
                {
                    other.GetComponentInParent<BoltEntity>().GetState<IItemState>().OwnerID = state.OwnerID;
                    totemBehaviour.SetTotemKinematic(true);
                    totemBehaviour.SetParent(_throwPositions.BackPosition);
                }
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
            if (Input.GetButtonDown(Constants.Input.UseItem) ||
                Input.GetButtonDown(Constants.Input.UseItemForward) ||
                Input.GetButtonDown(Constants.Input.UseItemBackward))
            {
                UseTotem();
            }
        }

        // PRIVATE

        private void SetTotem(GameObject totem)
        {
            _inventory.StopAllCoroutines(); // Stop any anti-spam routine
            _inventory.CanUseItem = false;
        }

        private void UseTotem()
        {
            TotemThrown totemThrownEvent = TotemThrown.Create();
            totemThrownEvent.OwnerID = state.OwnerID;
            totemThrownEvent.Send();
        }
    }
}
