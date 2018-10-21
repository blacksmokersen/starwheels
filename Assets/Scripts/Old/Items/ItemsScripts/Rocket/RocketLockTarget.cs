using System.Collections;
using UnityEngine;
using Kart;

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
            if (other.gameObject.tag == Constants.Tag.KartTrigger && activated && ActualTarget == null)
            {
                /*
                if (other.GetComponentInParent<KartHub>().kartInventory != Owner)
                {
                    ActualTarget = other.gameObject;
                    StartCoroutine(GetComponentInParent<RocketBehaviour>().StartQuickTurn());
                }
                */
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == Constants.Tag.KartTrigger && activated)
            {
                //if (other.GetComponentInParent<KartHub>().kartInventory == Owner) return;
                if (IsKartIsCloserThanActualTarget(other.gameObject) || ActualTarget == null)
                {
                    ActualTarget = other.gameObject;
                    actualTargetDistance = Vector3.Distance(transform.position, ActualTarget.transform.position);
                    StartCoroutine(GetComponentInParent<RocketBehaviour>().StartQuickTurn());
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
