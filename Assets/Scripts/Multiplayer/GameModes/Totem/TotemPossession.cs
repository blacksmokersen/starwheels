using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;
using ThrowingSystem;

namespace GameModes.Totem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TotemPicker))]
    public class TotemPossession : GlobalEventListener
    {
        [Header("Settings")]
        [SerializeField] private TotemSettings _totemSettings;

        [Header("Disallow on Totem Picking")]
        [SerializeField] private Items.Inventory _inventory;
        [SerializeField] private Abilities.AbilitySetter _abilitySetter;

        [Header("Unity Events")]
        public UnityEvent OnTotemGet;
        public UnityEvent OnTotemThrown;

        private bool _canUseItemAndAbility = true; // Local bool for possession (to compensate lag)

        // BOLT

        public override void OnEvent(TotemThrown evnt)
        {
            var totem = GetTotem();

            if (evnt.OwnerID == GetTotemOwnerID() || evnt.OwnerID == -1) // The owner of the totem is throwing it || or it is a totem reset
            {
                totem.GetComponent<Totem>().UnsetParent();

                if (BoltNetwork.isServer)
                {
                    var kartThrowing = MyExtensions.KartExtensions.GetKartWithID(evnt.OwnerID);
                    if (kartThrowing)
                    {
                        Direction throwingDirection = evnt.ForwardDirection ? Direction.Forward : Direction.Backward;
                        kartThrowing.GetComponentInChildren<ThrowableLauncher>().Throw(totem.GetComponent<Throwable>(), throwingDirection);
                    }
                }

                if(!_canUseItemAndAbility) // I was the totem owner but threw it
                {
                    StartCoroutine(CanUseItemAndAbility(true));
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
                GetTotem().GetComponent<Totem>().SetParent(kartTotemSlot, evnt.NewOwnerID);

                if (evnt.KartEntity.isOwner && _canUseItemAndAbility) // If I am the new owner of the totem and ready to pick it up
                {
                    StartCoroutine(CanUseItemAndAbility(false));
                }
                else if (!_canUseItemAndAbility) // If I was the old owner of the totem
                {
                    StartCoroutine(CanUseItemAndAbility(true));
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
            var totemBehaviour = GetTotem().GetComponent<Totem>();

            if (kartOwnerID == totemBehaviour.LocalOwnerID) // The totem owner has been hit
            {
                totemBehaviour.UnsetParent();

                if (evnt.PlayerEntity.isOwner) // If I was the totem owner
                {
                    StartCoroutine(CanUseItemAndAbility(true));
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

        private BoltEntity GetTotemEntity()
        {
            return GetTotem().GetComponent<BoltEntity>();
        }

        private int GetTotemOwnerID()
        {
            return GetTotemEntity().GetState<IItemState>().OwnerID;
        }

        private IEnumerator CanUseItemAndAbility(bool b)
        {
            var ability = _abilitySetter.GetCurrentAbility();

            if (b == true)
            {
                yield return new WaitForSeconds(3f);
            }
            else
            {
                _inventory.StopAllCoroutines(); // Stop any anti-spam routine
                ability.StopAllCoroutines();
                ability.Reload();
            }

            ability.CanUseAbility = b;
            _inventory.CanUseItem = b;
            _canUseItemAndAbility = b;
        }
    }
}
