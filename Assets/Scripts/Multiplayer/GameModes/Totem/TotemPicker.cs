using UnityEngine;
using Bolt;
using ThrowingSystem;
using Items;

namespace GameModes.Totem
{
    public class TotemPicker : EntityBehaviour<IKartState> , IControllable
    {
        [SerializeField] private Inventory _inventory;

        // MONOBEHAVIOUR

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.isServer && other.CompareTag(Constants.Tag.TotemPickup)) // Server sees a player collide with totem trigger
            {
                var totemBehaviour = other.GetComponentInParent<TotemBehaviour>();
                if (totemBehaviour.CanBePickedUp)
                {
                    TotemPicked totemPickedEvent = TotemPicked.Create();
                    totemPickedEvent.NewOwnerID = state.OwnerID;
                    totemPickedEvent.Send();
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
            totemThrownEvent.ForwardDirection = FindObjectOfType<ThrowableLauncher>().GetThrowingDirection() != Direction.Backward ; // TO DO BETTER
            totemThrownEvent.Send();
        }
    }
}
