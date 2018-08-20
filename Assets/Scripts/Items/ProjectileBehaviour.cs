using UnityEngine;
using Kart;
using System.Collections;

namespace Items
{
    [RequireComponent(typeof(AudioSource))]
    public class ProjectileBehaviour : ItemBehaviour
    {
        [Header("Projectile parameters")]
        public float Speed;        

        [Header("Ground parameters")]
        public AirStates AirState = AirStates.InAir;
        public float DistanceForGrounded;
        public float LocalGravity;

        [Header("Particles Effects")]
        public ParticleSystem CollisionParticles;

        [Header("Sounds")]
        public AudioClip LaunchSound;
        public AudioClip FlySound;
        public AudioClip PlayerHitSound;
        public AudioClip CollisionSound;       

        protected Rigidbody rb;
        protected KartInventory owner;

        private AudioSource audioSource;
        private const float ownerImmunityDuration = 1f;
        private bool ownerImmuned = true;

        protected void Awake()
        {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }

        protected void Start()
        {
            
        }

        protected void Update()
        {
            CheckGrounded();
        }

        protected void FixedUpdate()
        {
            NormalizeSpeed();
            if (rb.useGravity == true)
                ApplyLocalGravity();
        }

        #region ItemLogic

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
            if (Physics.Raycast(transform.position, Vector3.down, out hit, DistanceForGrounded, 1 << LayerMask.NameToLayer(Constants.GroundLayer)))
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
            rb.AddForce(Vector3.down * LocalGravity);
        }

        public override void Spawn(KartInventory kart, Directions direction)
        {
            if (direction == Directions.Forward || direction == Directions.Default)
            {
                var rot = new Vector3(0,kart.transform.rotation.eulerAngles.y,0);
                transform.rotation = Quaternion.Euler(rot);
                var vel = kart.transform.forward * Speed;
                vel.y = 0;
                rb.velocity = vel;
                transform.position = kart.ItemPositions.FrontPosition.position;
            }
            else if (direction == Directions.Backward)
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

        IEnumerator StartOwnerImmunity()
        {
            ownerImmuned = true;
            yield return new WaitForSeconds(ownerImmunityDuration);
            ownerImmuned = false;
        }

        public void CheckCollision(Collider other)
        {
            if (owner == null || (other.gameObject.GetComponentInParent<KartHub>().kartInventory == owner && ownerImmuned)) return;

            other.gameObject.GetComponentInParent<KartEvents>().OnHit();
            CollisionParticles.Emit(2000);
            PlayPlayerHitSound();
            DestroyObject();    
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartRigidBodyTag)
            {
                CheckCollision(other);
            }
        }

        #endregion

        #region Audio
        public void PlayLaunchSound()
        {
            audioSource.PlayOneShot(PlayerHitSound);
        }

        public void PlayFlySound()
        {
            audioSource.clip = FlySound;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void PlayPlayerHitSound()
        {
            AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);
        }

        public void PlayCollisionSound()
        {
            AudioSource.PlayClipAtPoint(CollisionSound, transform.position);
        }
        #endregion
    }
}
