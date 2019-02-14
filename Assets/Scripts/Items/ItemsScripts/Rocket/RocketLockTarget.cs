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

        public float _targetTime = 2f;
        public bool _startTimer = false;
        public bool _notFinalDirection = true;

        private void Start()
        {
            StartCoroutine(LookForTarget());
        }

        private void Update()
        {
            if (_startTimer) { }
            _targetTime -= Time.deltaTime;

            if (_targetTime <= 0.0f && _notFinalDirection)
            {
                Debug.Log("RocketTimerEnd");
                timerEnded();
                _notFinalDirection = false;
            }
        }


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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.Tag.KartHealthHitBox && _activated && CurrentTarget == null)
            {
                var otherPlayer = other.GetComponentInParent<Player>();

                if (entity.isAttached && state.Team != otherPlayer.Team.GetColor())
                {
                    _startTimer = true;
                    CurrentTarget = other.gameObject;
                    StartCoroutine(GetComponentInParent<RocketBehaviour>().StartQuickTurn());
                }

            }
        }

        private void OnTriggerStay(Collider other)
        {

            if (other.gameObject.tag == Constants.Tag.KartHealthHitBox && _activated)
            {
                var otherPlayer = other.GetComponentInParent<Player>();

                if (entity.isAttached && state.Team != otherPlayer.Team.GetColor())
                {
                    if (CurrentTarget == null) // || IsKartIsCloserThanActualTarget(other.gameObject))
                    {
                        CurrentTarget = other.gameObject;
                        StartCoroutine(GetComponentInParent<RocketBehaviour>().StartQuickTurn());
                    }
                }
            }
        }

        private void timerEnded()
        {
            GetComponentInParent<RocketBehaviour>().NoCircleSecurityQuickTurn();
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
