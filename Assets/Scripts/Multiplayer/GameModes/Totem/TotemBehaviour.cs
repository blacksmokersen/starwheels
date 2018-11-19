using System.Collections;
using UnityEngine;
using Bolt;

namespace GameModes.Totem
{
    public class TotemBehaviour : EntityBehaviour<IItemState>
    {
        [Header("Slowdown Settings")]
        [SerializeField] private float _slowdownFactor = 0.98f;
        [SerializeField] private float _stopMagnitudeThreshold = 0.1f;

        private bool _isPossessed = false;
        
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

        public override void ControlGained()
        {
            Debug.Log("Gained control on totem");
            _isSlowingDown = false;
            SetTotemKinematic(true);
        }

        public override void SimulateController()
        {

        }

        public override void ControlLost()
        {            
            gameObject.transform.SetParent(null);
            SetTotemKinematic(false);
            Debug.Log("Lost control on totem");
        }

        // PUBLIC

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
                GetComponent<SphereCollider>().enabled = false;                
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
    }
}
