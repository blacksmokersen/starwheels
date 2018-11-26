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
            if (!totem) Debug.LogError("Totem was not found.");

            totem.GetComponent<TotemBehaviour>().SetTotemKinematic(false);
            totem.GetComponent<TotemBehaviour>().SetParent(null);

            if (BoltNetwork.isServer)
            {
                var totemEntity = totem.GetComponent<BoltEntity>();
                if (evnt.OwnerID == totemEntity.GetState<IItemState>().OwnerID)
                {
                    var kartThrowing = MyExtensions.KartExtensions.GetKartWithID(evnt.OwnerID);
                    kartThrowing.GetComponentInChildren<ThrowableLauncher>().Throw(totemEntity.GetComponent<Throwable>());
                    totemEntity.GetState<IItemState>().OwnerID = -1;
                }
            }
            else
            {
                totem.GetComponent<BoltEntity>().ReleaseControl();
            }
        }

        public override void OnEvent(TotemPicked evnt)
        {
            var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);
            if (!totem) Debug.LogError("Totem was not found.");

            var kart = MyExtensions.KartExtensions.GetKartWithID(evnt.NewOwnerID);

            if (kart)
            {
                if(BoltNetwork.isClient) totem.GetComponent<BoltEntity>().TakeControl();
                var backPosition = kart.GetComponentInChildren<ThrowPositions>().BackPosition;
                totem.GetComponent<TotemBehaviour>().SetParent(backPosition);
                totem.GetComponent<TotemBehaviour>().SetTotemKinematic(true);
                totem.transform.localPosition = Vector3.zero;
            }
            else
            {
                Debug.LogError("Owner not found.");
                totem.transform.SetParent(null);
            }
        }
    }
}
