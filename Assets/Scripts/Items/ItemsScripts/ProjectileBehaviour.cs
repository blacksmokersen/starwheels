using UnityEngine;
using Kart;
using System.Collections;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace Items
{
    [RequireComponent(typeof(AudioSource))]
    public class ProjectileBehaviour : ItemBehaviour
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
        protected KartInventory owner;

        private const float _ownerImmunityDuration = 0.5f;
        private bool _ownerImmune = true;
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

        public override void Spawn(KartInventory kart, Direction direction)
        {
            if (direction == Direction.Forward || direction == Direction.Default)
            {
                var rot = new Vector3(0, kart.transform.rotation.eulerAngles.y, 0);
                transform.rotation = Quaternion.Euler(rot);
                var vel = kart.transform.forward * Speed;
                vel.y = 0;
                rb.velocity = vel;
                transform.position = kart.ItemPositions.FrontPosition.position;
            }
            else if (direction == Direction.Backward)
            {
                var rot = new Vector3(0, kart.transform.rotation.eulerAngles.y + 180, 0); // Apply 180° turn
                transform.rotation = Quaternion.Euler(rot);
                rb.velocity = -kart.transform.forward * Speed;
                transform.position = kart.ItemPositions.BackPosition.position;
            }
            owner = kart;
            StartCoroutine(StartOwnerImmunity());
            PlayLaunchSound();
            PlayFlySound();
        }

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
            if (OwnerIsSet() && !IsOwnerAndImmune(kartCollisionObject))
            {
                if (!kartCollisionObject.GetComponentInParent<KartHealthSystem>().IsInvincible && !IsOnSameTeam(kartCollisionObject))
                {
                    if(!IsOwner(kartCollisionObject))
                        SendOwnerSuccessfulHitEvent();
                    SendTargetOnHitEvent(kartCollisionObject);
                }
                CollisionParticles.Emit(ParticlesToEmitOnHit);
                PlayPlayerHitSound();
                if (DestroyAfterHit)
                {
                    DestroyObject();
                }
            }
        }

        private bool OwnerIsSet()
        {
            return owner != null;
        }

        private bool IsOwner(GameObject other)
        {
            var otherKartInventory = other.GetComponentInParent<KartHub>().kartInventory;
            return otherKartInventory == owner;
        }

        private bool IsOwnerAndImmune(GameObject other)
        {
            var otherKartInventory = other.GetComponentInParent<KartHub>().kartInventory;
            return otherKartInventory == owner && _ownerImmune;
        }
        private bool IsOnSameTeam(GameObject other)
        {
            var otherTeam = other.GetComponentInParent<PhotonView>().Owner.GetTeam();
            return otherTeam == photonView.Owner.GetTeam() && !IsOwner(other);
        }

        private void SendOwnerSuccessfulHitEvent()
        {
            //owner.gameObject.GetComponentInParent<KartEvents>().HitSomeoneElse();
        }

        private void SendTargetOnHitEvent(GameObject kartCollisionObject)
        {
            kartCollisionObject.gameObject.GetComponentInParent<KartEvents>().CallOnHitEvent();
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
