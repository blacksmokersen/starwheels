using UnityEngine;
using Kart;
using System.Collections;

namespace Items
{
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

        protected Rigidbody rb;
        protected KartInventory owner;

        private const float ownerImmunityDuration = 0.5f;
        private bool ownerImmuned = true;

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
            rb.velocity = rb.velocity.normalized * Speed;
        }

        public override void Spawn(KartInventory kart, Directions direction)
        {
            transform.rotation = kart.transform.rotation;
            if(direction == Directions.Forward)
            {
                rb.velocity = kart.transform.forward * Speed;
                transform.position = kart.ItemPositions.FrontPosition.position;
            }
            else if(direction == Directions.Backward)
            {
                rb.velocity = -kart.transform.forward * Speed;
                transform.position = kart.ItemPositions.BackPosition.position;
            }
            owner = kart;
        }

        private void CheckGrounded()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, DistanceForGrounded, 1 << LayerMask.NameToLayer(Constants.GroundLayer)))
            {
                rb.useGravity = false;

                var velocity = rb.velocity;
                velocity.y = 0;
                rb.velocity = velocity;

                var position = transform.position;
                position.y = hit.point.y + DistanceForGrounded - 0.1f;
                transform.position = position;
            }
            else
            {
                rb.useGravity = true;
                ApplyLocalGravity();
            }
        }

        public void ApplyLocalGravity()
        {
            rb.AddForce(Vector3.down * LocalGravity);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartRigidBodyTag)
            {
                CheckCollision(other);
            }
        }

        public void CheckCollision(Collider other)
        {
            if (!(other.gameObject == owner && ownerImmuned))
            { 
                other.gameObject.GetComponentInParent<KartHealthSystem>().HealthLoss();
                CollisionParticles.Emit(2000);
                DestroyObject();
            }
        }

        IEnumerator OwnerImmunity()
        {
            ownerImmuned = true;
            yield return new WaitForSeconds(ownerImmunityDuration);
            ownerImmuned = false;
        }
    }
}
