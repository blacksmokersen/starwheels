using UnityEngine;
using Bolt;
using ThrowingSystem;

namespace GameModes.Totem
{
    [RequireComponent(typeof(TotemPicker))]
    public class TotemPossession : GlobalEventListener
    {
        [SerializeField] private Items.Inventory _inventory;

        private bool _canUseItems = true; // Local bool for possession

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

                if(!_canUseItems)
                {
                    CanUseItem(true);
                }
            }
        }

        public override void OnEvent(TotemPicked evnt)
        {
            var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);
            if (!totem) return;

            var kart = MyExtensions.KartExtensions.GetKartWithID(evnt.NewOwnerID);

            if (kart)
            {
                var newOwnerKart = MyExtensions.KartExtensions.GetKartWithID(evnt.NewOwnerID);
                var kartTotemSlot = newOwnerKart.GetComponentInChildren<TotemSlot>().transform;
                totem.GetComponent<TotemBehaviour>().SetParent(kartTotemSlot, evnt.NewOwnerID);
            }
            else
            {
                Debug.LogError("Owner not found.");
                totem.transform.SetParent(null);
            }

            if (evnt.KartEntity.isOwner)
            {
                CanUseItem(false);
            }
            else if (!_canUseItems)
            {
                CanUseItem(true);
            }
        }

        public override void OnEvent(PlayerHit evnt)
        {
            var kartOwnerID = evnt.PlayerEntity.GetState<IKartState>().OwnerID;
            var totemBehaviour = GameObject.FindGameObjectWithTag(Constants.Tag.Totem).GetComponent<TotemBehaviour>();

            Debug.Log("Kart Owner ID  : " + kartOwnerID);
            Debug.Log("Local totem Owner ID  : " + totemBehaviour.LocalOwnerID);

            if (kartOwnerID == totemBehaviour.LocalOwnerID)
            {
                totemBehaviour.UnsetParent();
                if (evnt.PlayerEntity.isOwner)
                {
                    CanUseItem(true);
                }
            }
        }

        // PRIVATE

        public void CanUseItem(bool b)
        {
            Debug.Log("Can use item : " + b);
            _inventory.StopAllCoroutines(); // Stop any anti-spam routine
            _inventory.CanUseItem = b;
            _canUseItems = b;
        }
    }
}
