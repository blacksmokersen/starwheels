using System.Collections;
using UnityEngine;
using Bolt;

namespace GameModes.Totem
{
    [DisallowMultipleComponent]
    public class TotemBehaviour : EntityBehaviour<IItemState>
    {
        public bool CanBePickedUp = true;
        public int LocalOwnerID = -1;

        [Header("Slowdown Settings")]
        [SerializeField] private float _slowdownFactor = 0.98f;
        [SerializeField] private float _stopMagnitudeThreshold = 0.1f;

        private Rigidbody _rb;
        private bool _isSlowingDown = false;
        private Transform _parent;

        // CORE

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_parent != null)
            {
                Debug.Log("I am controller");
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
            Debug.LogError("Totem detached from game.");
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
                _rb.velocity *= _slowdownFactor;

                if (_rb.velocity.magnitude < _stopMagnitudeThreshold)
                {
                    _isSlowingDown = false;
                    _rb.velocity = Vector3.zero;
                }
            }
        }

        private IEnumerator AntiPickSpamRoutine()
        {
            CanBePickedUp = false;
            yield return new WaitForSeconds(1f);
            CanBePickedUp = true;
        }
    }
}
