using UnityEngine;
using Bolt;
using ThrowingSystem;

namespace GameModes.Totem
{
    [RequireComponent(typeof(TotemPicker))]
    public class TotemPossession : GlobalEventListener
    {
        // BOLT

        public override void OnEvent(TotemThrown evnt)
        {
            var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);
            if (!totem) return;

            var totemEntity = totem.GetComponent<BoltEntity>();

            if (evnt.OwnerID == totemEntity.GetState<IItemState>().OwnerID || evnt.OwnerID == -1) // The owner of the totem is throwing it || or it is a totem reset
            {
                totem.GetComponent<TotemBehaviour>().UnsetParent();

                if (BoltNetwork.isServer)
                {
                    var kartThrowing = MyExtensions.KartExtensions.GetKartWithID(evnt.OwnerID);
                    if (kartThrowing)
                    {
                        Direction throwingDirection = evnt.ForwardDirection ? Direction.Forward : Direction.Backward;
                        kartThrowing.GetComponentInChildren<ThrowableLauncher>().Throw(totemEntity.GetComponent<Throwable>(), throwingDirection);
                    }
                }
            }
        }

        public override void OnEvent(TotemPicked evnt)
        {
            var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);
            if (!totem) Debug.LogError("Totem was not found.");

            var kart = MyExtensions.KartExtensions.GetKartWithID(evnt.NewOwnerID);

            if (kart)
            {
                var backPosition = kart.GetComponentInChildren<ThrowPositions>().BackPosition;
                totem.GetComponent<TotemBehaviour>().SetParent(backPosition, evnt.NewOwnerID);
            }
            else
            {
                Debug.LogError("Owner not found.");
                totem.transform.SetParent(null);
            }
        }
    }
}
