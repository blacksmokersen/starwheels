using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DiskBehaviour : ProjectileBehaviour
    {
        [Header("Disk parameters")]
        public int ReboundsBeforeEnd;
               
        public ParticleSystem collisionParticles;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartRigidBodyTag)
            {
                other.gameObject.GetComponentInParent<Kart.KartHealthSystem>().HealthLoss();
                DestroyObject();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                Vector3 contactPoint = collision.contacts[0].point;
                collisionParticles.transform.position = contactPoint;
                collisionParticles.Emit(600);
                ReboundsBeforeEnd--;
                if (ReboundsBeforeEnd <= 0)
                {
                    DestroyObject();
                }
            }
        }
    }
}
