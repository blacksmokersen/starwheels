using UnityEngine;
using Bolt;
using ThrowingSystem;

namespace GameModes.Totem
{
    public class TotemPicker : EntityBehaviour<IKartState> , IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Throwing System")]
        [SerializeField] private ThrowingDirection _throwingDirection;


        // MONOBEHAVIOUR

        private void Update()
        {
            if (entity.isAttached && entity.isControllerOrOwner)
            {
                MapInputs();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.IsServer && other.CompareTag(Constants.Tag.TotemPickup)) // Server sees a player collide with totem trigger
            {
                var totemBehaviour = other.GetComponentInParent<Totem>();
                if (totemBehaviour.CanBePickedUp && state.CanPickTotem && totemBehaviour.LocalOwnerID != state.OwnerID)
                {
                    TotemPicked totemPickedEvent = TotemPicked.Create();
                    totemPickedEvent.KartEntity = entity;
                    totemPickedEvent.NewOwnerID = state.OwnerID;
                    totemPickedEvent.Send();
                }
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled)
            {
                if (Input.GetButtonDown(Constants.Input.UseItem) ||
                    Input.GetButtonDown(Constants.Input.UseItemForward) ||
                    Input.GetButtonDown(Constants.Input.UseItemBackward))
                {
                    UseTotem();
                }
            }
        }

        // PRIVATE

        private void UseTotem()
        {
            TotemThrown totemThrownEvent = TotemThrown.Create();
            totemThrownEvent.KartEntity = entity;
            totemThrownEvent.OwnerID = state.OwnerID;
            totemThrownEvent.ForwardDirection = _throwingDirection.LastDirectionUp != Direction.Backward ; // TO DO BETTER
            totemThrownEvent.Send();
        }
    }
}
