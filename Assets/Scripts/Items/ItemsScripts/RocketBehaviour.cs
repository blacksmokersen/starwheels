using System.Collections;
using UnityEngine;

namespace Items
{
    public class RocketBehaviour : MonoBehaviour
    {
        [Header("Rocket parameters")]
        public float Speed;
        public float MaxAngle;
        public GameObject Target;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            StartCoroutine(LookForTarget());
        }

        private void LateUpdate()
        {
            if(Target != null)
            {

            }
        }

        public void SetDirection(Vector3 direction)
        {
            rb.velocity = direction * Speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartTag)
            {
                Destroy(gameObject);
            }
        }

        IEnumerator LookForTarget()
        {
            yield return new WaitForSeconds(1f);
            var karts = GameObject.FindGameObjectsWithTag(Constants.KartTag);
            GameObject closestKart = karts[0];
            float closestDistance = Vector3.Distance(transform.position, karts[0].transform.position);
            foreach (var kart in karts)
            {
                var distanceToKart = Vector3.Distance(transform.position, kart.transform.position);

                if (distanceToKart < closestDistance)
                {
                    closestKart = kart;
                    closestDistance = distanceToKart;
                }
            }
            Target = closestKart;
        }

    }
}