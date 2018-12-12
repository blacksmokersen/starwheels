using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DiskBehaviour : ProjectileBehaviour
    {
        [Header("Disk parameters")]
        public int ReboundsBeforeEnd;
        public int ParticlesToEmit;

        [SerializeField] ItemActivationBehaviour itemActivationBehaviour;

        private void OnCollisionEnter(Collision collision)
        {
            if (BoltNetwork.isServer)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
                {
                    itemActivationBehaviour.Activated = true;
                    Vector3 contactPoint = collision.contacts[0].point;
                    CollisionParticles.transform.position = contactPoint;
                    CollisionParticles.Emit(ParticlesToEmit);
                    ReboundsBeforeEnd--;
                    PlayCollisionSound();
                    Debug.Log("Bounds left : " + ReboundsBeforeEnd);

                    if (ReboundsBeforeEnd <= 0)
                    {
                        DestroyEntity destroyEntityEvent = DestroyEntity.Create();
                        destroyEntityEvent.Entity = entity;
                        destroyEntityEvent.Send();
                    }
                }
            }
        }
    }
}
