using UnityEngine;
using Bolt;
using ThrowingSystem;

namespace Gamemodes.Totem
{
    public class TotemThrower : EntityBehaviour<IKartState>, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Throwing System")]
        [SerializeField] private ThrowingDirection _throwingDirection;

        private TotemOwnership _totemOwnership;

        // MONOBEHAVIOUR

        private void Update()
        {
            if (entity.isAttached && entity.isControllerOrOwner)
            {
                MapInputs();
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
                    if (!_totemOwnership)
                    {
                        _totemOwnership = TotemHelpers.GetTotemComponent();
                    }
                    if (_totemOwnership)
                    {
                        ThrowTotem();
                    }
                }
            }
        }

        // PRIVATE

        private void ThrowTotem()
        {
            if (_totemOwnership.IsLocalOwner(state.OwnerID) && _totemOwnership.IsSynchronized())
            {
                TotemThrown totemThrownEvent = TotemThrown.Create();
                totemThrownEvent.KartEntity = entity;
                totemThrownEvent.OwnerID = state.OwnerID;
                totemThrownEvent.ForwardDirection = _throwingDirection.LastDirectionUp != Direction.Backward; // TO DO BETTER
                totemThrownEvent.Send();
            }
        }
    }
}
