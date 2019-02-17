﻿using UnityEngine;
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

        public bool _isLocalOwner = false; // Local bool for possession (to compensate lag)

        [Header("Totem References (Debug)")]
        [SerializeField] private Totem _totemBehaviour;
        [SerializeField] private GameObject _totemObjectRoot;

        private bool _referencesSet = false;

        // CORE

        private void Start()
        {
            StartCoroutine(SynchronizationRoutine());
        }

        // BOLT


        public override void OnEvent(TotemThrown evnt)
        {
            if (_referencesSet && (evnt.OwnerID == _totemBehaviour.ServerOwnerID || evnt.OwnerID == -1)) // The owner of the totem is throwing it || or it is a totem reset
            {
                _totemBehaviour.UnsetParent();

                if (BoltNetwork.IsServer) // The server make the player throw the totem
                {
                    var kartThrowing = MyExtensions.KartExtensions.GetKartWithID(evnt.OwnerID);
                    if (kartThrowing)
                    {
                        Direction throwingDirection = evnt.ForwardDirection ? Direction.Forward : Direction.Backward;
                        kartThrowing.GetComponentInChildren<ThrowableLauncher>().Throw(_totemObjectRoot.GetComponent<Throwable>(), throwingDirection);
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
            SetNewOwner(evnt.NewOwnerID);
        }

        public override void OnEvent(PlayerHit evnt)
        {
            if (_referencesSet && evnt.VictimEntity != null && evnt.VictimEntity.isAttached) // Verify that the player hasn't been destroyed
            {
                if (evnt.VictimID == _totemBehaviour.LocalOwnerID) // The totem owner has been hit
                {
                    _totemBehaviour.UnsetParent();

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
                yield return new WaitForSeconds(0.25f);
                TrySettingTotem();
                SynchronizeTotemOwner();
            }
        }

        private void SynchronizeTotemOwner()
        {
            if (_totemObjectRoot && _totemBehaviour)
            {
                if (!_totemBehaviour.IsSynchronized())
                {
                    SetNewOwner(_totemBehaviour.ServerOwnerID);
                }
            }
        }

        private void SetNewOwner(int newOwnerID)
        {
            var kart = MyExtensions.KartExtensions.GetKartWithID(newOwnerID);

            if (kart)
            {
                var kartTotemSlot = kart.GetComponentInChildren<TotemSlot>().transform;
                _totemBehaviour.SetParent(kartTotemSlot, newOwnerID);

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
                _totemBehaviour.UnsetParent();
            }
        }

        private void TrySettingTotem()
        {
            if (!_totemObjectRoot)
            {
                _totemObjectRoot = TotemHelpers.FindTotem();
            }
            else if(_totemObjectRoot && !_totemBehaviour)
            {
                _totemBehaviour = _totemObjectRoot.GetComponent<Totem>();
                _referencesSet = true;
            }
        }
    }
}
