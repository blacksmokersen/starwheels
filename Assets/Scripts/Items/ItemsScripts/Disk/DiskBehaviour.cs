﻿using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DiskBehaviour : ProjectileBehaviour
    {
        [Header("Disk parameters")]
        public int ReboundsBeforeEnd;
        public int ParticlesToEmit;

        [HideInInspector] public bool CanHitOwner;

        //BOLT

        public override void Attached()
        {
            DestroyObject(30f);
        }

        //PUBLIC

        public void LaunchMode(int mode)
        {
            if (mode == 10)
                DestroyObject(0.1f);
        }

        //PRIVATE

        private void OnCollisionEnter(Collision collision)
        {
            if (BoltNetwork.IsServer)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
                {
                    CanHitOwner = true;
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
