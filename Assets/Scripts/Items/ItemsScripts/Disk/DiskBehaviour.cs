using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DiskBehaviour : ProjectileBehaviour
    {
        [Header("Disk parameters")]
        public int ReboundsBeforeEnd;
        public int ParticlesToEmit;

        [HideInInspector] public bool CanHitOwner;

        [SerializeField] ItemActivationBehaviour itemActivationBehaviour;

        private IItemState _itemState;

        private void Update()
        {
            /*
            if (_itemState != null)
            {
                Debug.LogErrorFormat("State : {0}", _itemState.OwnerID);
                Debug.LogErrorFormat("Team : {0}", _itemState.Team);
            }
            */
        }

        //BOLT

        public override void Attached()
        {
            DestroyObject(20);
            entity.TryFindState<IItemState>(out _itemState);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (BoltNetwork.IsServer)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
                {
                    CanHitOwner = true;
                    itemActivationBehaviour.Activated = true;
                    Vector3 contactPoint = collision.contacts[0].point;
                    CollisionParticles.transform.position = contactPoint;
                    CollisionParticles.Emit(ParticlesToEmit);
                    ReboundsBeforeEnd--;
                    PlayCollisionSound();

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
