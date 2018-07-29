using System.Collections;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class RocketLockTarget : MonoBehaviour
    {
        [Header("Targeting system")]
        public float SecondsBeforeSearchingTarget;
        public KartInventory Owner;
        public GameObject ActualTarget;

        private float actualTargetDistance;
        private bool activated;

        private void Start()
        {
            StartCoroutine(LookForTarget());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartRigidBodyTag && activated && ActualTarget == null)
            {
                if (other.GetComponentInParent<KartInventory>() != Owner)
                {
                    ActualTarget = other.gameObject;
                }            
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == Constants.KartRigidBodyTag && activated)
            {
                if (other.GetComponentInParent<KartInventory>() == Owner) return;
                if (IsKartIsCloserThanActualTarget(other.gameObject))
                {
                    ActualTarget = other.gameObject;
                    actualTargetDistance = Vector3.Distance(transform.position, ActualTarget.transform.position);
                }
            }
        }

        IEnumerator LookForTarget()
        {
            activated = false;
            yield return new WaitForSeconds(SecondsBeforeSearchingTarget); // For X seconds the rocket goes straight forward       
            activated = true;
        }

        public bool IsKartIsCloserThanActualTarget(GameObject kart)
        {
            return Vector3.Distance(transform.position, kart.transform.position) < actualTargetDistance;
        }
    }
}