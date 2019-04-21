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

        [Header("Dependencies")]
        [SerializeField] private RocketBehaviour _rocketBehaviour;

        private bool _activated = false;

        // CORE

        private void OnTriggerEnter(Collider other)
        {
            if (_rocketBehaviour.CurrentTarget == null && other.gameObject.tag == Constants.Tag.KartHealthHitBox && _activated)
            {
                var otherPlayer = other.GetComponentInParent<Player>();

                if (entity.isAttached && state.Team.ToTeam() != otherPlayer.Team)
                {
                    _rocketBehaviour.SetTarget(other.transform);
                    Debug.Log("A");
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (_rocketBehaviour.CurrentTarget == null && other.gameObject.tag == Constants.Tag.KartHealthHitBox && _activated)
            {
                var otherPlayer = other.GetComponentInParent<Player>();

                if (entity.isAttached && state.Team.ToTeam() != otherPlayer.Team)
                {
                    _rocketBehaviour.SetTarget(other.transform);
                    Debug.Log("B");
                }
            }
        }

        // PRIVATE

        private bool IsKartIsCloserThanActualTarget(GameObject kart)
        {
            return Vector3.Distance(_origin.position, kart.transform.position) < Vector3.Distance(_origin.position, CurrentTarget.transform.position);
        }
    }
}
