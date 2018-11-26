using System.Collections;
using UnityEngine;
using Bolt;

namespace GameModes.Totem
{
    public class TotemBehaviour : EntityBehaviour<IItemState>
    {
        public bool CanBePickedUp = true;

        [Header("Slowdown Settings")]
        [SerializeField] private float _slowdownFactor = 0.98f;
        [SerializeField] private float _stopMagnitudeThreshold = 0.1f;

        private Transform _parent;
        private Rigidbody _rb;
        private bool _isSlowingDown = false;

        // CORE

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
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

        public override void SimulateOwner()
        {
            if (_parent)
            {
                //transform.position = _parent.position;
            }
            else
            {
                Slowdown();
            }
        }

        // PUBLIC

        public void SetParent(Transform parent)
        {
            _parent = parent;
            transform.SetParent(parent);

            StartCoroutine(AntiPickSpamRoutine());

            if (parent == null)
            {
                _isSlowingDown = true;
            }
            else
            {
                _isSlowingDown = false;
            }
        }

        public void StartSlowdown()
        {
            StartCoroutine(SlowdownRoutine());
        }

        public void SetTotemKinematic(bool b)
        {
            if (b)
            {
                Debug.Log("Totem is kinematic.");
                GetComponent<Rigidbody>().isKinematic = true;
                //GetComponent<SphereCollider>().enabled = false; //USE LAYERS
            }
            else
            {
                Debug.Log("Totem is NOT kinematic.");
                GetComponent<Rigidbody>().isKinematic = false;
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
            }
            if (_rb.velocity.magnitude < _stopMagnitudeThreshold)
            {
                _isSlowingDown = false;
                _rb.velocity = Vector3.zero;
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
