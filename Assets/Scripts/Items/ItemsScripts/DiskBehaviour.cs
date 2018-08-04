using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DiskBehaviour : ProjectileBehaviour
    {
        [Header("Disk parameters")]
        public int ReboundsBeforeEnd;
        public int ParticlesToEmit;
       
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                Vector3 contactPoint = collision.contacts[0].point;
                CollisionParticles.transform.position = contactPoint;
                CollisionParticles.Emit(ParticlesToEmit);
                ReboundsBeforeEnd--;
                if (ReboundsBeforeEnd <= 0)
                {
                    DestroyObject();
                }
            }
        }
    }
}
