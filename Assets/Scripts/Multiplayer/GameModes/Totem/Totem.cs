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
        public int ServerOwnerID { get { return state.OwnerID; } }

        [Header("Unity Events")]
        public UnityEvent OnParentSet;
        public UnityEvent OnParentUnset;

        [Header("Settings")]
        [SerializeField] private TotemSettings _totemSettings;
        [SerializeField] private Collider _physicalCollider;

        private Rigidbody _rb;
        private bool _isSlowingDown = false;
        private Coroutine _slowdownCoroutine;
        private Transform _parent;

        // CORE

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_parent == null)
            {
                Slowdown();
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
                _isSlowingDown = false;
            }
            if (_slowdownCoroutine != null)
            {
                StopCoroutine(_slowdownCoroutine);
            }

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
                _slowdownCoroutine = StartCoroutine(SlowdownRoutine());
            }
            else
            {
                entity.ReleaseControl();
            }

            LocalOwnerID = -1;
            CanBePickedUp = true;
            _parent = null;
            _rb.velocity = Vector3.zero;

            if (OnParentUnset != null) OnParentUnset.Invoke();
        }

        public void StartAntiSpamCoroutine()
        {
            StartCoroutine(AntiPickSpamRoutine());
        }

        // PRIVATE

        private IEnumerator SlowdownRoutine()
        {
            yield return new WaitForSeconds(_totemSettings.SecondsBeforeSlowdown);
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
