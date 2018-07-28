using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DiskBehaviour : ProjectileBehaviour
    {
        [Header("Disk parameters")]
        public int ReboundsBeforeEnd;
        
       
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                Vector3 contactPoint = collision.contacts[0].point;
                CollisionParticles.transform.position = contactPoint;
                CollisionParticles.Emit(600);
                ReboundsBeforeEnd--;
                if (ReboundsBeforeEnd <= 0)
                {
                    DestroyObject();
                }
            }
        }
    }
}
