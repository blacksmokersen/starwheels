using UnityEngine;
using Bolt;
using ThrowingSystem;

namespace Gamemodes.Totem
{
    public class OnTotemEventListener : GlobalEventListener
    {
        [Header("Ownership")]
        [SerializeField] private TotemOwnership _totemOwnership;

        // BOLT

        public override void OnEvent(TotemPicked evnt)
        {
            _totemOwnership.SetNewOwner(evnt.NewOwnerID);
        }

        public override void OnEvent(PlayerHit evnt)
        {
            if (evnt.VictimID == _totemOwnership.LocalOwnerID) // The totem owner has been hit
            {
                _totemOwnership.UnsetOwner();
            }
        }

        public override void OnEvent(TotemThrown evnt)
        {
            if (BoltNetwork.IsServer)
            {
                if (evnt.OwnerID != -1 && evnt.OwnerID == _totemOwnership.ServerOwnerID) // The owner of the totem is throwing it || or it is a totem reset
                {
                    SendSuccessfulThrowEvent();

                    var kartThrowing = SWExtensions.KartExtensions.GetKartWithID(evnt.OwnerID);
                    if (kartThrowing)
                    {
                        Direction throwingDirection = evnt.ForwardDirection ? Direction.Forward : Direction.Backward;
                        kartThrowing.GetComponentInChildren<ThrowableLauncher>().Throw(GetComponent<Throwable>(), throwingDirection); // The server make the player throw the totem
                    }
                }
            }
            if (evnt.OwnerID == -1)
            {
                _totemOwnership.UnsetOwner();
            }
        }

        // PRIVATE

        private void SendSuccessfulThrowEvent()
        {
            TotemThrown totemThrownEvent = TotemThrown.Create();
            totemThrownEvent.OwnerID = -1;
            totemThrownEvent.Send();
        }
    }
}
