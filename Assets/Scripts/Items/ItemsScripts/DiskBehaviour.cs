using UnityEngine;
using Kart;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DiskBehaviour : ItemBehaviour
    {
        [Header("Disk parameters")]
        public float Speed;
        public int ReboundsBeforeEnd;

        [Header("Ground parameters")]
        public AirStates AirState = AirStates.InAir;
        public float DistanceForGrounded;
        public float LocalGravity;

        private Rigidbody rb;
        public ParticleSystem collisionParticles;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        
        public override void SetOwner(KartInventory kart)
        {
            rb.velocity = kart.transform.forward * Speed;
            transform.position = kart.ItemPositions.FrontPosition.position;
        }

        private void FixedUpdate()
        {
            rb.velocity = rb.velocity.normalized * Speed;
        }

        private void Update()
        {
            CheckGrounded();
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartRigidBodyTag)
            {
                other.gameObject.GetComponentInParent<Kart.KartHealthSystem>().HealthLoss();
                DestroyObject();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Vector3 contactPoint = collision.contacts[0].point;
            collisionParticles.transform.position = contactPoint;
            collisionParticles.Emit(600);
            ReboundsBeforeEnd--;
            if (ReboundsBeforeEnd <= 0)
            {
                DestroyObject();
            }
        }

        private void DestroyObject()
        {
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
