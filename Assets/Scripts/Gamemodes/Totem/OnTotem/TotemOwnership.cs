using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Gamemodes.Totem
{
    [DisallowMultipleComponent]
    public class TotemOwnership : EntityBehaviour<IItemState>
    {
        [Header("Ownership")]
        public bool CanBePickedUp = true;
        public int OldOwnerID = -1;
        public int LocalOwnerID = -1;
        public int ServerOwnerID
        {
            get
            {
                if (entity.isAttached)
                {
                    return state.OwnerID;
                }
                else
                {
                    return -1;
                }
            }
        }

        [Header("Unity Events")]
        public UnityEvent OnParentSet;
        public UnityEvent OnParentUnset;

        [Header("Settings")]
        [SerializeField] private TotemSettings _totemSettings;

        private Transform _parent;

        // CORE

        private void Start()
        {
            StartCoroutine(SynchronizationRoutine());
        }

        private void LateUpdate()
        {
            if (_parent != null)
            {
                transform.position = _parent.position;
            }
        }

        // BOLT

        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);

            if (entity.isOwner)
            {
                state.OwnerID = -1;
            }
        }

        // PUBLIC

        public bool IsLocalOwner(int id)
        {
            return LocalOwnerID == id;
        }

        public bool IsServerOwner(int id)
        {
            return ServerOwnerID == id;
        }

        public bool IsSynchronized()
        {
            return LocalOwnerID == ServerOwnerID;
        }

        public void SetNewOwner(int newOwnerID)
        {
            if (entity.isOwner)
            {
                state.OwnerID = newOwnerID;
            }
            else
            {
                entity.TakeControl();
            }
            OldOwnerID = LocalOwnerID;
            LocalOwnerID = newOwnerID;
            StartCoroutine(AntiPickSpamRoutine());
            SetParentTransform(LocalOwnerID);

            if (OnParentSet != null)
            {
                OnParentSet.Invoke();
            }
        }

        public void UnsetOwner()
        {
            if (entity.isOwner)
            {
                state.OwnerID = -1;
            }
            else
            {
                entity.ReleaseControl();
            }
            OldOwnerID = LocalOwnerID;
            LocalOwnerID = -1;
            CanBePickedUp = true;
            UnsetParentTransform();

            if (OnParentUnset != null)
            {
                OnParentUnset.Invoke();
            }
        }

        public void StartAntiSpamCoroutine()
        {
            StartCoroutine(AntiPickSpamRoutine());
        }

        // PRIVATE

        private void SetParentTransform(int id)
        {
            var kart = SWExtensions.KartExtensions.GetKartWithID(id);

            if (kart)
            {
                var kartTotemSlot = kart.GetComponentInChildren<TotemSlot>();
                _parent = kartTotemSlot.transform;
            }
            else
            {
                _parent = null;
            }
        }

        private void UnsetParentTransform()
        {
            _parent = null;
        }

        private IEnumerator SynchronizationRoutine()
        {
            while (Application.isPlaying)
            {
                yield return new WaitForSeconds(0.25f);
                SynchronizeTotemOwner();
            }
        }

        private void SynchronizeTotemOwner()
        {
            if (!IsSynchronized())
            {
                SetNewOwner(ServerOwnerID); // TODO : Send info to previous owner
            }
        }

        private IEnumerator AntiPickSpamRoutine()
        {
            CanBePickedUp = false;
            yield return new WaitForSeconds(_totemSettings.SecondsBeforeCanBePickedAgain);
            CanBePickedUp = true;
        }
    }
}
