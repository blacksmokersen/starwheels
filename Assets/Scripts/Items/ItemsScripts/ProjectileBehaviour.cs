using UnityEngine;
using Kart;
using System.Collections;

namespace Items
{
    [RequireComponent(typeof(AudioSource))]
    public class ProjectileBehaviour : MonoBehaviour
    {
        #region Variables
        [Header("Projectile parameters")]
        public float Speed;
        public bool DestroyAfterHit = true;

        [Header("Ground parameters")]
        public float DistanceForGrounded;
        public float LocalGravity;

        [Header("Particles Effects")]
        public ParticleSystem CollisionParticles;
        public int ParticlesToEmitOnHit = 2000;

        [Header("Sounds")]
        public AudioSource LaunchSource;
        public AudioSource FlySource;
        public AudioSource PlayerHitSource;
        public AudioSource CollisionSource;

        protected Rigidbody rb;
        protected Inventory owner;

        private const float _ownerImmunityDuration = 0.5f;
        private bool _ownerImmune = true;
        [SerializeField] private float aimAngle = 45;
        #endregion

        protected void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        protected void Update()
        {
            CheckGrounded();
        }

        protected void FixedUpdate()
        {
            NormalizeSpeed();
            ApplyLocalGravity();
        }

        #region Instantiation

        private IEnumerator StartOwnerImmunity()
        {
            _ownerImmune = true;
            yield return new WaitForSeconds(_ownerImmunityDuration);
            _ownerImmune = false;
        }
        #endregion

        #region Physics

        protected void NormalizeSpeed()
        {
            var newVelocity = rb.velocity;
            newVelocity.y = 0;
            newVelocity = newVelocity.normalized * Speed;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }

        private void CheckGrounded()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, DistanceForGrounded, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
            {
                rb.useGravity = false;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                transform.position = new Vector3(transform.position.x, hit.point.y + DistanceForGrounded - 0.1f, transform.position.z);
            }
            else
            {
                rb.useGravity = true;
            }
        }

        public void ApplyLocalGravity()
        {
            if (rb.useGravity == true)
            {
                rb.AddForce(Vector3.down * LocalGravity);
            }
        }
        #endregion

        #region Collisions

        public void CheckCollision(GameObject kartCollisionObject)
        {
            /*
            if (OwnerIsSet() && !IsOwnerAndImmune(kartCollisionObject))
            {
                if (!kartCollisionObject.GetComponentInParent<KartHealthSystem>().IsInvincible && !IsOnSameTeam(kartCollisionObject))
                {
                    if (!IsOwner(kartCollisionObject))
                    {
                        var target = kartCollisionObject.GetComponentInParent<PhotonView>().Owner;
                        SendOwnerSuccessfulHitEvent(target);
                        // TODO: Do this cleaner :)
                        owner.gameObject.GetComponentInParent<KartHub>().GetComponentInChildren<KartGameMode>().IncreaseScore();
                    }
                    SendTargetOnHitEvent(kartCollisionObject);
                }

                CollisionParticles.Emit(ParticlesToEmitOnHit);
                PlayPlayerHitSound();

                if (DestroyAfterHit)
                {
                    DestroyObject();
                }
            }
            */
        }

        #endregion

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
            MyExtensions.Audio.PlayClipObjectAndDestroy(PlayerHitSource);
        }

        protected void PlayCollisionSound()
        {
            MyExtensions.Audio.PlayClipObjectAndDestroy(CollisionSource);
        }
        #endregion
    }
}
