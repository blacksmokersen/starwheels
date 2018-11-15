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
            if (BoltNetwork.isServer)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
                {
                    Vector3 contactPoint = collision.contacts[0].point;
                    CollisionParticles.transform.position = contactPoint;
                    CollisionParticles.Emit(ParticlesToEmit);
                    ReboundsBeforeEnd--;
                    PlayCollisionSound();

                    if (ReboundsBeforeEnd <= 0)
                    {
                        DestroyObject();
                    }
                }
            }
        }
    }
}
