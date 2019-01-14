using UnityEngine;
using UnityEngine.Events;
using Bolt;
using ThrowingSystem;
using System.Collections;

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

        private bool _isLocalOwner = false; // Local bool for possession (to compensate lag)

        // CORE

        private void Start()
        {
            StartCoroutine(SynchronizationRoutine());
        }

        // BOLT

        public override void OnEvent(TotemThrown evnt)
        {
            var totem = TotemHelpers.FindTotem();

            if (evnt.OwnerID == TotemHelpers.GetTotemOwnerID() || evnt.OwnerID == -1) // The owner of the totem is throwing it || or it is a totem reset
            {
                totem.GetComponent<Totem>().UnsetParent();

                if (BoltNetwork.IsServer) // The server make the player throw the totem
                {
                    totem.GetComponent<Totem>().StartAntiSpamCoroutine();

                    var kartThrowing = MyExtensions.KartExtensions.GetKartWithID(evnt.OwnerID);
                    if (kartThrowing)
                    {
                        Direction throwingDirection = evnt.ForwardDirection ? Direction.Forward : Direction.Backward;
                        kartThrowing.GetComponentInChildren<ThrowableLauncher>().Throw(totem.GetComponent<Throwable>(), throwingDirection);
                    }
                }

                if(_isLocalOwner) // I was the totem owner but threw it
                {
                    _isLocalOwner = false;
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

                if (evnt.KartEntity.isOwner && !_isLocalOwner) // If I am the new owner of the totem and ready to pick it up
                {
                    _isLocalOwner = true;
                    OnTotemGet.Invoke();
                }
                else if (_isLocalOwner) // If I was the old owner of the totem
                {
                    _isLocalOwner = false;
                    OnTotemLost.Invoke();
                }
            }
        }

        public override void OnEvent(PlayerHit evnt)
        {
            if (evnt.VictimEntity != null && evnt.VictimEntity.isAttached) // Verify that the player hasn't been destroyed
            {
                var totemBehaviour = TotemHelpers.GetTotemComponent();

                if (evnt.VictimID == totemBehaviour.LocalOwnerID) // The totem owner has been hit
                {
                    totemBehaviour.UnsetParent();

                    if (evnt.VictimEntity.isOwner) // If I was the totem owner
                    {
                        _isLocalOwner = false;
                        OnTotemLost.Invoke();
                    }
                }
            }
        }

        // PRIVATE

        private IEnumerator SynchronizationRoutine()
        {
            while (Application.isPlaying)
            {
                yield return new WaitForSeconds(0.2f);
                SynchronizeTotemOwner();
            }
        }

        private void SynchronizeTotemOwner()
        {
            var totem = TotemHelpers.FindTotem();

            if (totem)
            {
                Debug.Log("Totem is found.");
                var totemLocalOwnerID = totem.GetComponent<Totem>().LocalOwnerID;
                var totemServerOwnerID = TotemHelpers.GetTotemOwnerID();


                Debug.LogFormat("Local Owner {0} || Server Owner {1}", totemLocalOwnerID, totemServerOwnerID);
                if (totemLocalOwnerID != totemServerOwnerID)
                {
                    Debug.Log("DIFFERENCE!");
                    var kart = MyExtensions.KartExtensions.GetKartWithID(totemServerOwnerID);

                    if (kart)
                    {
                        var kartTotemSlot = kart.GetComponentInChildren<TotemSlot>().transform;
                        totem.GetComponent<Totem>().SetParent(kartTotemSlot, totemServerOwnerID);

                        if (kart.GetComponent<BoltEntity>().isOwner && !_isLocalOwner) // If I am the new owner of the totem and ready to pick it up
                        {
                            _isLocalOwner = true;
                            OnTotemGet.Invoke();
                        }
                        else if (_isLocalOwner) // If I was the old owner of the totem
                        {
                            _isLocalOwner = false;
                            OnTotemLost.Invoke();
                        }
                    }
                    else
                    {
                        totem.GetComponent<Totem>().UnsetParent();
                    }
                }
            }
        }
    }
}
