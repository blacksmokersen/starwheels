using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace GameModes.Totem
{
    [DisallowMultipleComponent]
    public class Totem : EntityBehaviour<IItemState>
    {
        [Header("Ownership")]
        public bool CanBePickedUp = true;
        public int LocalOwnerID = -1;

        [Header("Unity Events")]
        public UnityEvent OnParentSet;
        public UnityEvent OnParentUnset;

        [Header("Settings")]
        [SerializeField] private TotemSettings _totemSettings;

        private Rigidbody _rb;
        private bool _isSlowingDown = false;
        private Transform _parent;

        // CORE

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void LateUpdate()
        {
            if (_parent != null)
            {
                transform.position = _parent.position;
            }
            else
            {
                Slowdown();
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

        public void SetParent(Transform parent, int newOwnerID)
        {
            if(entity.isOwner) state.OwnerID = newOwnerID;

            LocalOwnerID = newOwnerID;
            entity.TakeControl();
            _parent = parent;
            FreezeTotem(true);
            _isSlowingDown = false;
            StartCoroutine(AntiPickSpamRoutine());

            if (OnParentSet != null) OnParentSet.Invoke();
            Debug.Log("Set totem locally.");
        }

        public void UnsetParent()
        {
            if (entity.isOwner) state.OwnerID = -1;
            else entity.ReleaseControl();

            LocalOwnerID = -1;
            CanBePickedUp = true;
            _parent = null;
            FreezeTotem(false);
            StopAllCoroutines();
            _isSlowingDown = true;

            if (OnParentUnset != null) OnParentUnset.Invoke();
            Debug.Log("Unset totem locally.");
        }

        public void FreezeTotem(bool b)
        {
            var rb = GetComponent<Rigidbody>();
            if (b)
            {
                rb.isKinematic = true;
                //GetComponent<SphereCollider>().enabled = false; //USE LAYERS
            }
            else
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
                GetComponent<SphereCollider>().enabled = true;
            }
        }

        // PRIVATE

        private IEnumerator SlowdownRoutine()
        {
            yield return new WaitForSeconds(1.5f);
            _isSlowingDown = true;
        }

        private void Slowdown()
        {
            if (_isSlowingDown)
            {
                _rb.velocity *= _totemSettings.SlowdownFactor;

                if (_rb.velocity.magnitude < _totemSettings.StopMagnitudeThreshold)
                {
                    _isSlowingDown = false;
                    _rb.velocity = Vector3.zero;
                }
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
