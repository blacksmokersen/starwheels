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
            if (BoltNetwork.isServer)
            {
                Debug.Log("TotemThrown event sent from " + evnt.OwnerID);

                var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);
                if (!totem)
                {
                    Debug.LogError("Totem was not found.");
                }

                var totemEntity = totem.GetComponent<BoltEntity>();
                if (evnt.OwnerID == totemEntity.GetState<IItemState>().OwnerID)
                {
                    var kartThrowing = MyExtensions.KartExtensions.GetKartWithID(evnt.OwnerID);
                    var totemBehaviour = totem.GetComponent<TotemBehaviour>();
                    totemBehaviour.SetParent(null);
                    totemBehaviour.SetTotemKinematic(false);
                    kartThrowing.GetComponentInChildren<ThrowableLauncher>().Throw(totemEntity.GetComponent<Throwable>());
                    totemEntity.GetState<IItemState>().OwnerID = -1;
                }
            }
        }
    }
}
