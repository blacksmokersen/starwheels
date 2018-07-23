using System.Collections;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class RocketLockTarget : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartTag)
            {
                GetComponentInParent<RocketBehaviour>().Target = other.gameObject;
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
            GetComponentInParent<RocketBehaviour>().Target = closestKart.gameObject;
        }
    }
}