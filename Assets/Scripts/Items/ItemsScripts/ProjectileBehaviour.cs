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
        [SerializeField] private FloatVariable _speed;

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
        public void PlayLaunchSound()
        {
            LaunchSource.Play();
        }

        public void PlayFlySound()
        {
            FlySource.loop = true;
            FlySource.Play();
        }

        public void PlayPlayerHitSound()
        {
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(PlayerHitSource);
        }

        public void PlayCollisionSound()
        {
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(CollisionSource);
        }
        #endregion

        // PROTECTED

        protected void NormalizeSpeed()
        {
            var newVelocity = rb.velocity;
            newVelocity.y = 0;
            newVelocity = newVelocity.normalized * _speed.Value;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }
    }
}
