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
        private CloakPortalsActivator _cloakPortalActivator;
        private Coroutine _cloakPortalCoroutine;
        private LineRenderer _lineRenderer;

        private GameObject _fakeKartVisualInPortal;
        private CloakPortalCameraBehaviour _portalCamera;

        private GameObject _camPos;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _cloakPortalActivator = GetComponentInParent<CloakPortalsActivator>();
            _camPos = GameObject.FindWithTag("CloakPortalCamPos");
        }

        private void Start()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _targetPortal.transform.position);
        }

        public void TeleportPlayerToTargetPortal(GameObject kart, GameObject targetPortal)
        {
            _health = kart.GetComponentInChildren<Health.Health>();
            _kartRigidbody = kart.GetComponentInChildren<Rigidbody>();
            _kartMeshDisabler = kart.GetComponentInChildren<CloakAbility>().KartMeshDisabler;

            StartCoroutine(PortalTravelBehaviour(kart, targetPortal));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.KartCollider))
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



        private void PortalCameraSwitch()
        {
            _portalCamera = GameObject.Find("PlayerCamera").GetComponent<CloakPortalCameraBehaviour>();
        }


        private IEnumerator PortalTravelBehaviour(GameObject kart, GameObject targetPortal)
        {
            //   kart.GetComponentInChildren<CloakAbility>().CanUsePortals = false;

            kart.GetComponentInChildren<CloakAbility>().DisableCanUsePortalForXSeconds(_cloakPortalActivator.TravelTime + 1);

            kart.GetComponent<Common.ControllableDisabler>().DisableAllInChildren();

            PortalCameraSwitch();

            _kartMeshDisabler.DisableKartMeshes(false);
            kart.GetComponentInChildren<CloakAbility>().CloakEffect.SetActive(false);
            _health.SetInvincibility();



            _fakeKartVisualInPortal = _cloakPortalActivator.FakeKartVisualInPortal;
            _fakeKartVisualInPortal.SetActive(true);
            _fakeKartVisualInPortal.transform.position = Vector3.zero;
            _fakeKartVisualInPortal.transform.rotation = transform.rotation;


            _portalCamera.StartPortalTransferCamAnimation(_camPos, kart, _cloakPortalActivator.CameraDistanceInPortal);





            _camPos.GetComponentInParent<CloakPortalTransferBehaviour>().StartLerping(_cloakPortalActivator.TravelTime);


            yield return new WaitForSeconds(_cloakPortalActivator.TravelTime);


            /*

             var _currentTimer = 0f;

            while (_currentTimer < _cloakPortalActivator.TravelTime)
            {
                _fakeKartVisualInPortal.transform.position = Vector3.Lerp(this.transform.position, _targetPortal.transform.position, _currentTimer / _cloakPortalActivator.TravelTime);
                _currentTimer += Time.deltaTime;
                yield return null;
            }
            */


            _portalCamera.StopPortalTransferCamAnimation(kart);

            _fakeKartVisualInPortal.SetActive(false);
           //  yield return new WaitForSeconds(_cloakPortalActivator.TravelTime);

            var y = _targetPortal.transform.position.y + 0f;
            _kartRigidbody.transform.position = new Vector3(_targetPortal.transform.position.x, y, _targetPortal.transform.position.z);
            _kartRigidbody.transform.rotation = transform.rotation;

            var _currentTimer = 0f;
            while (_currentTimer < 0.5f)
            {
                _kartRigidbody.AddRelativeForce(Vector3.forward * 2000, ForceMode.Force);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _kartMeshDisabler.EnableKartMeshes(false);
            _health.UnsetInvincibility();
            kart.GetComponent<Common.ControllableDisabler>().EnableAllInChildren();
            kart.GetComponentInChildren<CloakAbility>().CloakEffect.SetActive(true);


            /*
            yield return new WaitForSeconds(_cloakPortalActivator.TimeToUseThisPortalAgain);
            kart.GetComponentInChildren<CloakAbility>().CanUsePortals = true;
            */

        }
    }
}
