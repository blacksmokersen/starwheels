using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Bolt;
using UnityEngine;

namespace Abilities
{
    public class CloakPortalBehaviour : EntityBehaviour<IItemState>
    {
        [SerializeField] GameObject _targetPortal;

        [Header("Unity Events")]
        public UnityEvent AtStartCloakTeleport;
        public UnityEvent AtEndCloakTeleport;

        private KartMeshDisabler _kartMeshDisabler;
        private Health.Health _health;

        public void TeleportPlayerToTargetPortal(GameObject kart,GameObject targetPortal)
        {
            _kartMeshDisabler = kart.GetComponentInChildren<CloakAbility>().KartMeshDisabler;
            _health = kart.GetComponentInChildren<Health.Health>();
            StartCoroutine(PortalTravelBehaviour(kart, targetPortal));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.Kart))
            {
                Debug.Log(other.transform.root.gameObject);
                if (other.transform.root.gameObject.GetComponentInChildren<CloakAbility>().CanUsePortals)
                {
                    TeleportPlayerToTargetPortal(other.transform.root.gameObject, _targetPortal);
                }
            }
        }

        private IEnumerator PortalTravelBehaviour(GameObject kart, GameObject targetPortal)
        {
            _kartMeshDisabler.DisableKartMeshes(false);
            _health.SetInvincibility();
            yield return new WaitForSeconds(0.5f);
            _kartMeshDisabler.EnableKartMeshes(false);
            _health.UnsetInvincibility();
            /*
            var y = _tpBack.transform.position.y + 5f;
            _rb.transform.position = new Vector3(_tpBack.transform.position.x, y, _tpBack.transform.position.z);
            _rb.transform.rotation = GetKartRotation();
            */
        }

    }
}
