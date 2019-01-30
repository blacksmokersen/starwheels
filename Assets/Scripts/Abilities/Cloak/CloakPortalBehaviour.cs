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
        private Rigidbody _kartRigidbody;
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            _lineRenderer.SetPosition(0,transform.position);
            _lineRenderer.SetPosition(1,_targetPortal.transform.position);
        }

        public void TeleportPlayerToTargetPortal(GameObject kart,GameObject targetPortal)
        {
            _kartMeshDisabler = kart.GetComponentInChildren<CloakAbility>().KartMeshDisabler;
            _health = kart.GetComponentInChildren<Health.Health>();
            _kartRigidbody = kart.GetComponentInChildren<Rigidbody>();
            StartCoroutine(PortalTravelBehaviour(kart, targetPortal));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.Kart))
            {
                if (other.transform.root.gameObject.GetComponentInChildren<CloakAbility>() != null)
                {
                    if (other.transform.root.gameObject.GetComponentInChildren<CloakAbility>().CanUsePortals)
                    {
                        TeleportPlayerToTargetPortal(other.transform.root.gameObject, _targetPortal);
                    }
                }
            }
        }

        private IEnumerator PortalTravelBehaviour(GameObject kart, GameObject targetPortal)
        {
            _kartMeshDisabler.DisableKartMeshes(false);
            _health.SetInvincibility();

            var y = _targetPortal.transform.position.y + 5f;
            _kartRigidbody.transform.position = new Vector3(_targetPortal.transform.position.x, y, _targetPortal.transform.position.z);
          //  _kartRigidbody.transform.rotation = GetKartRotation();

            yield return new WaitForSeconds(0.5f);


            _kartMeshDisabler.EnableKartMeshes(false);
            _health.UnsetInvincibility();


        }

    }
}
