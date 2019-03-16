using UnityEngine;
using Bolt;

namespace Gamemodes.Totem
{
    public class TotemPicker : EntityBehaviour<IKartState>
    {
        // CORE

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.IsServer && other.CompareTag(Constants.Tag.TotemPickup)) // Server sees a player collide with totem trigger
            {
                var totemOwnership = other.GetComponentInParent<TotemOwnership>();
                var totemColor = other.GetComponentInParent<TotemColorChanger>();

                if (entity.isAttached &&
                    totemOwnership.CanBePickedUp &&
                    state.CanPickTotem &&
                    totemOwnership.LocalOwnerID != state.OwnerID
                    && (totemColor.CurrentColor == state.Team || totemColor.ColorIsDefault()))
                {
                    SendTotemPickedEvent(totemOwnership.ServerOwnerID);
                }
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
