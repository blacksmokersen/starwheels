using UnityEngine;
using Bolt;

namespace Gamemodes.Totem
{
    public class TotemPicker : EntityBehaviour<IKartState>
    {
        public float SecondsSinceLastPick = 0f;

        // CORE

        private void Update()
        {
            if (BoltNetwork.IsServer)
            {
                SecondsSinceLastPick += Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.IsServer && other.CompareTag(Constants.Tag.TotemPickup)) // Server sees a player collide with totem trigger
            {
                var totemOwnership = other.GetComponentInParent<TotemOwnership>();
                var totemColor = other.GetComponentInParent<TotemColorChanger>();

                if (entity.isAttached &&
                    totemOwnership.CanBePickedUp && // The anti-spam routine is over
                    state.CanPickTotem && // Kart is charged
                    !(state.OwnerID == totemOwnership.OldOwnerID && SecondsSinceLastPick < 1f) && // It's not the same kart that threw it
                    totemOwnership.LocalOwnerID != state.OwnerID && // It's not the current owner of the totem
                    (totemColor.CurrentColor == state.Team || totemColor.ColorIsDefault())) // The kart has the same color as totem or totem color is default
                {
                    SendTotemPickedEvent(totemOwnership.ServerOwnerID);
                }
            }
        }

        // PUBLIC

        public void ResetSecondsSinceLastPick()
        {
            if (BoltNetwork.IsServer)
            {
                SecondsSinceLastPick = 0f;
            }
        }

        // PRIVATE

        private void SendTotemPickedEvent(int oldOwnerID)
        {
            TotemPicked totemPickedEvent = TotemPicked.Create();
            totemPickedEvent.KartEntity = entity;
            totemPickedEvent.OldOwnerID = oldOwnerID;
            totemPickedEvent.NewOwnerID = state.OwnerID;
            totemPickedEvent.Send();
        }
    }
}
