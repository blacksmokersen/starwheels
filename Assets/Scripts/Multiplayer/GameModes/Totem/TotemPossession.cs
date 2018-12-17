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

        [Header("Unity Events")]
        public UnityEvent OnTotemGet;
        public UnityEvent OnTotemLost;

        private bool isLocalOwner = false; // Local bool for possession (to compensate lag)

        // BOLT

        public override void OnEvent(TotemThrown evnt)
        {
            var totem = TotemHelpers.FindTotem();

            if (evnt.OwnerID == TotemHelpers.GetTotemOwnerID() || evnt.OwnerID == -1) // The owner of the totem is throwing it || or it is a totem reset
            {
                totem.GetComponent<Totem>().UnsetParent();

                if (BoltNetwork.isServer) // The server make the player throw the totem
                {
                    totem.GetComponent<Totem>().StartAntiSpamCoroutine();

                    var kartThrowing = MyExtensions.KartExtensions.GetKartWithID(evnt.OwnerID);
                    if (kartThrowing)
                    {
                        Direction throwingDirection = evnt.ForwardDirection ? Direction.Forward : Direction.Backward;
                        kartThrowing.GetComponentInChildren<ThrowableLauncher>().Throw(totem.GetComponent<Throwable>(), throwingDirection);
                    }
                }

                if(isLocalOwner) // I was the totem owner but threw it
                {
                    isLocalOwner = false;
                    OnTotemLost.Invoke();
                }
            }
        }

        public override void OnEvent(TotemPicked evnt)
        {
            var kart = MyExtensions.KartExtensions.GetKartWithID(evnt.NewOwnerID);

            if (kart)
            {
                var kartTotemSlot = kart.GetComponentInChildren<TotemSlot>().transform;
                TotemHelpers.GetTotemComponent().SetParent(kartTotemSlot, evnt.NewOwnerID);

                if (evnt.KartEntity.isOwner && !isLocalOwner) // If I am the new owner of the totem and ready to pick it up
                {
                    isLocalOwner = true;
                    OnTotemGet.Invoke();
                }
                else if (isLocalOwner) // If I was the old owner of the totem
                {
                    isLocalOwner = false;
                    OnTotemLost.Invoke();
                }
            }
        }

        public override void OnEvent(PlayerHit evnt)
        {
            var kartOwnerID = evnt.PlayerEntity.GetState<IKartState>().OwnerID;
            var totemBehaviour = TotemHelpers.GetTotemComponent();

            if (kartOwnerID == totemBehaviour.LocalOwnerID) // The totem owner has been hit
            {
                totemBehaviour.UnsetParent();

                if (evnt.PlayerEntity.isOwner) // If I was the totem owner
                {
                    isLocalOwner = false;
                    OnTotemLost.Invoke();
                }
            }
        }
    }
}
