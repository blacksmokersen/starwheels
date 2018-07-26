using UnityEngine;

namespace Items
{
    public class RocketBehaviour : ProjectileBehaviour
    {
        [Header("Rocket parameters")]
        public float MaxAngle;
        public GameObject Target;
        public float TurningTorque;

        private new void FixedUpdate()
        {
            base.FixedUpdate();
            TurnTowardTarget();
        }

        void TurnTowardTarget()
        {
            if(Target != null)
            {
                if (Target.transform.position.x < 0) // If the target is on the left we turn to the left
                {
                    rb.AddRelativeTorque(Vector3.up * TurningTorque);
                }
                else // If the target is on the right we turn to the right
                {
                    rb.AddRelativeTorque(Vector3.down * TurningTorque);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                DestroyObject();
            }
        }

    }
}