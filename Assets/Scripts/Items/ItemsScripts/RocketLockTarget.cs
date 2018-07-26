using System.Collections;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class RocketLockTarget : MonoBehaviour
    {
        private GameObject ActualTarget;
        private float ActualTargetDistance;

        private void Start()
        {
            StartCoroutine(LookForTarget());
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == Constants.KartTag)
            {
                if (IsKartIsCloserThanActualTarget(other.gameObject))
                {
                    ActualTarget = other.gameObject;
                    ActualTargetDistance = Vector3.Distance(transform.position, ActualTarget.transform.position);
                }
            }
        }

        IEnumerator LookForTarget()
        {
            yield return new WaitForSeconds(1f); // For 1s the rocket goes straight forward
            while (ActualTarget != null)
            {
                yield return new WaitForSeconds(0.5f);
            }
            SetRocketTarget(ActualTarget);
        }

        public bool IsKartIsCloserThanActualTarget(GameObject kart)
        {
            return Vector3.Distance(transform.position, kart.transform.position) < ActualTargetDistance;
        }

        private void SetRocketTarget(GameObject target)
        {
            GetComponentInParent<RocketBehaviour>().Target = target;
        }
    }
}