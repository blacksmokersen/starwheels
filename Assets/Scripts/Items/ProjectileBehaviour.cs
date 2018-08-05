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

        private const float ownerImmunityDuration = 1f;
        private bool ownerImmuned = true;

        protected void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        protected void Start()
        {
            StartCoroutine(StartOwnerImmunity());
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
                transform.rotation = kart.transform.rotation;
                rb.velocity = kart.transform.forward * Speed;
                transform.position = kart.ItemPositions.FrontPosition.position;
            }
            else if (direction == Directions.Backward)
            {
                var rot = kart.transform.rotation.eulerAngles;
                rot = new Vector3(rot.x, rot.y + 180, rot.z); // Apply 180° turn
                transform.rotation = Quaternion.Euler(rot);
                rb.velocity = -kart.transform.forward * Speed;
                transform.position = kart.ItemPositions.BackPosition.position;
            }
            owner = kart;
        }

        IEnumerator StartOwnerImmunity()
        {
            ownerImmuned = true;
            yield return new WaitForSeconds(ownerImmunityDuration);
            ownerImmuned = false;
        }

        public void CheckCollision(Collider other)
        {
            if (other.gameObject.GetComponentInParent<KartHub>().kartInventory == owner && ownerImmuned) return;

            other.gameObject.GetComponentInParent<KartEvents>().OnHit();
            CollisionParticles.Emit(2000);
            DestroyObject();
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartRigidBodyTag)
            {
                CheckCollision(other);
            }
        }
    }
}
