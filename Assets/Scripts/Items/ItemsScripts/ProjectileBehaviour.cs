using UnityEngine;
using System.Collections;
using Multiplayer;

namespace Items
{
    public class ProjectileBehaviour : NetworkDestroyable
    {
        [Header("Owner")]
        public Ownership Ownership;

        [Header("Projectile parameters")]
        public float Speed;

        [Header("Particles Effects")]
        public ParticleSystem CollisionParticles;
        public int ParticlesToEmitOnHit = 2000;

        [Header("Sounds")]
        public AudioSource LaunchSource;
        public AudioSource FlySource;
        public AudioSource PlayerHitSource;
        public AudioSource CollisionSource;

        protected Rigidbody rb;

        // CORE

        protected void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Ownership = GetComponent<Ownership>();
        }

        protected void FixedUpdate()
        {
            NormalizeSpeed();
        }

        // PUBLIC

        public void OnHit()
        {
            CollisionParticles.Emit(ParticlesToEmitOnHit);
            PlayPlayerHitSound();
        }

        #region Audio
        protected void PlayLaunchSound()
        {
            LaunchSource.Play();
        }

        protected void PlayFlySound()
        {
            FlySource.loop = true;
            FlySource.Play();
        }

        protected void PlayPlayerHitSound()
        {
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(PlayerHitSource);
        }

        protected void PlayCollisionSound()
        {
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(CollisionSource);
        }
        #endregion

        // PROTECTED

        protected void NormalizeSpeed()
        {
            var newVelocity = rb.velocity;
            newVelocity.y = 0;
            newVelocity = newVelocity.normalized * Speed;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }
    }
}
