using UnityEngine;
using Bolt;
using ThrowingSystem;

namespace GameModes.Totem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TotemPicker))]
    public class TotemPossession : GlobalEventListener
    {
        [SerializeField] private Items.Inventory _inventory;

        private bool _canUseItems = true; // Local bool for possession

        // BOLT

        public override void OnEvent(TotemThrown evnt)
        {
            var totem = GetTotem();

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
            var kart = MyExtensions.KartExtensions.GetKartWithID(evnt.NewOwnerID);

            if (kart)
            {
                var newOwnerKart = MyExtensions.KartExtensions.GetKartWithID(evnt.NewOwnerID);
                var kartTotemSlot = newOwnerKart.GetComponentInChildren<TotemSlot>().transform;
                GetTotem().GetComponent<TotemBehaviour>().SetParent(kartTotemSlot, evnt.NewOwnerID);

                if (evnt.KartEntity.isOwner)
                {
                    CanUseItem(false);
                }
                else if (!_canUseItems)
                {
                    CanUseItem(true);
                }
            }
            else
            {
                Debug.LogError("Owner not found !");
            }
        }

        public override void OnEvent(PlayerHit evnt)
        {
            var kartOwnerID = evnt.PlayerEntity.GetState<IKartState>().OwnerID;
            var totemBehaviour = GetTotem().GetComponent<TotemBehaviour>();

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

        private GameObject GetTotem()
        {
            var totem = GameObject.FindGameObjectWithTag(Constants.Tag.Totem);

            if (totem)
            {
                return totem;
            }
            else
            {
                Debug.LogError("Totem was not found !");
                return null;
            }
        }

        private void CanUseItem(bool b)
        {
            _inventory.StopAllCoroutines(); // Stop any anti-spam routine
            _inventory.CanUseItem = b;
            _canUseItems = b;
        }
    }
}
