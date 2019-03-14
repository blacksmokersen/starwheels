using System.Collections;
using UnityEngine;
using Multiplayer;
using Bolt;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class RocketLockTarget : EntityBehaviour<IItemState>
    {
        [Header("Targeting system")]
        public float SecondsBeforeSearchingTarget;
        public GameObject CurrentTarget = null;
        [SerializeField] private Transform _origin;

        private bool _activated = false;

        // MONO

        private void Start()
        {
            StartCoroutine(LookForTarget());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CurrentTarget == null && other.gameObject.tag == Constants.Tag.KartHealthHitBox && _activated)
            {
                var otherPlayer = other.GetComponentInParent<Player>();

                if (entity.isAttached && state.Team != otherPlayer.Team.GetColor())
                {
                    SetTarget(other.gameObject);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {

            if (CurrentTarget == null && other.gameObject.tag == Constants.Tag.KartHealthHitBox && _activated)
            {
                var otherPlayer = other.GetComponentInParent<Player>();

                if (entity.isAttached && state.Team != otherPlayer.Team.GetColor())
                {
                    SetTarget(other.gameObject);
                }
            }
        }

        // BOLT

        public override void SimulateOwner()
        {
            try
            {
                if (CurrentTarget == null || !CurrentTarget.activeInHierarchy)
                {
                    CurrentTarget = null;
                }
            }
            catch (MissingReferenceException)
            {
                CurrentTarget = null;
                Debug.Log("Resetting rocket target.");
            }
        }


        // PRIVATE

        private void SetTarget(GameObject target)
        {
            CurrentTarget = target;
            target.GetComponentInParent<Health.Health>().OnDeath.AddListener(() => { CurrentTarget = null; });
            StartCoroutine(GetComponentInParent<RocketBehaviour>().StartQuickTurn());
        }

        private bool IsKartIsCloserThanActualTarget(GameObject kart)
        {
            return Vector3.Distance(_origin.position, kart.transform.position) < Vector3.Distance(_origin.position, CurrentTarget.transform.position);
        }

        private IEnumerator LookForTarget()
        {
            _activated = false;
            yield return new WaitForSeconds(SecondsBeforeSearchingTarget); // For X seconds the rocket goes straight forward
            _activated = true;
        }
    }
}
