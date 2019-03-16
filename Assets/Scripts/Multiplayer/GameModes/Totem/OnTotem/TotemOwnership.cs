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
        public int LocalOwnerID = -1;
        public int ServerOwnerID { get { return state.OwnerID; } }

        [Header("Unity Events")]
        public UnityEvent OnParentSet;
        public UnityEvent OnParentUnset;

        [Header("Settings")]
        [SerializeField] private TotemSettings _totemSettings;
        [SerializeField] private Collider _physicalCollider;

        private Transform _parent;

        // CORE

        private void FixedUpdate()
        {
            if (_parent == null)
            {
                //Slowdown();
            }
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

        public override void Detached()
        {
            Debug.Log("Totem detached from game.");
        }

        // PUBLIC

        public bool IsSynchronized()
        {
            return LocalOwnerID == ServerOwnerID;
        }

        public void SetParent(Transform parent, int newOwnerID)
        {
            if (entity.isOwner)
            {
                state.OwnerID = newOwnerID;
                //_isSlowingDown = false;
            }
            /*
            if (_slowdownCoroutine != null)
            {
                StopCoroutine(_slowdownCoroutine);
            }
            */

            LocalOwnerID = newOwnerID;
            entity.TakeControl();
            _parent = parent;
            StartCoroutine(AntiPickSpamRoutine());

            if (OnParentSet != null) OnParentSet.Invoke();
        }

        public void UnsetParent()
        {
            if (entity.isOwner)
            {
                state.OwnerID = -1;
                //_slowdownCoroutine = StartCoroutine(SlowdownRoutine());
            }
            else
            {
                entity.ReleaseControl();
            }

            LocalOwnerID = -1;
            CanBePickedUp = true;
            _parent = null;
            //_rb.velocity = Vector3.zero;

            if (OnParentUnset != null) OnParentUnset.Invoke();
        }

        public void StartAntiSpamCoroutine()
        {
            StartCoroutine(AntiPickSpamRoutine());
        }

        // PRIVATE

        private IEnumerator AntiPickSpamRoutine()
        {
            CanBePickedUp = false;
            yield return new WaitForSeconds(_totemSettings.SecondsBeforeCanBePickedAgain);
            CanBePickedUp = true;
        }
    }
}
