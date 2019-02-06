using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Common.HUD;

namespace Abilities
{
    public class CloakAbility : Ability, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Unity Events")]
        public UnityEvent OnOwnerCloackSet;
        public UnityEvent OnOwnerCloackUnset;

        public UnityEvent OnCloackSet;
        public UnityEvent OnCloackUnset;

        public KartMeshDisabler KartMeshDisabler;

        [Header("Meshes and Animation")]
        public GameObject CloakEffect;
        [SerializeField] private GameObject[] _kartMeshes;
        //   [SerializeField] private GameObject _kartNamePlate;
        [SerializeField] private Animator _animator;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource _useCloakSound;
        [SerializeField] private AudioSource _endCloakSound;


        [HideInInspector] public bool CanDisableCloak;
        public bool CanUsePortals = false;

        private CloakSettings _cloakSettings;
        private Coroutine _cloakRoutine;

        private CloakPortalCameraBehaviour _portalCamera;

        private void Awake()
        {
            _cloakSettings = (CloakSettings)abilitySettings;

            if(GameObject.Find("PlayerCamera").GetComponent<CloakPortalCameraBehaviour>() != null)
            _portalCamera = GameObject.Find("PlayerCamera").GetComponent<CloakPortalCameraBehaviour>();
        }

        // BOLT

        private void Update()
        {
            if (entity.isAttached && entity.isControllerOrOwner && gameObject.activeInHierarchy)
            {
                MapInputs();
            }
        }

        public override void Detached()
        {
            StopAllCoroutines();
        }

        // PUBLIC

        public new void Reload()
        {
            CanUseAbility = true;
            StopAllCoroutines();
            UnsetCloack();
        }

        public void MapInputs()
        {
            if (Enabled && Input.GetButtonDown(Constants.Input.UseAbility))
            {
                if (CanUseAbility)
                    SendCloakEvent();
            }
        }

        public void Use()
        {
            _animator.SetTrigger("ActivateCloakEffect");
            _cloakRoutine = StartCoroutine(CloakDuration(_cloakSettings.CloakDuration));
            StartCoroutine(Cooldown());
        }

        public void DisableCloak()
        {
            UnsetCloack();
            StartCoroutine(Cooldown());
            if (_cloakRoutine != null)
                StopCoroutine(_cloakRoutine);
            // _animator.SetTrigger("ActivateCloakEffect");
        }

        public void SendUnCloakEvent()
        {
            if (CanDisableCloak)
            {
                var unCloakEvent = UnCloakAbilityEvent.Create();
                unCloakEvent.CanDisableCloak = CanDisableCloak;
                unCloakEvent.Entity = entity;
                unCloakEvent.Send();
            }
        }

        public void DisableCanUsePortalForXSeconds(float duration)
        {
            StartCoroutine(DisableCanUsePortalForXSecondsCoroutine(duration));
        }


        public void EnableCloakPortals()
        {
            CanUsePortals = true;

            _portalCamera.ShowPortals();
            /*
            GameObject[] portalsToActivate;
            portalsToActivate = GameObject.FindGameObjectsWithTag(Constants.Tag.CloakPortals);

            foreach (GameObject portal in portalsToActivate)
            {
                portal.GetComponent<CloakPortalsActivator>().EnablePortals();
            }
            */
        }

        public void DisableCloakPortals()
        {
            CanUsePortals = false;

            _portalCamera.HidePortals();
            /*
            GameObject[] portalsToDisable;
            portalsToDisable = GameObject.FindGameObjectsWithTag(Constants.Tag.CloakPortals);

            foreach (GameObject portal in portalsToDisable)
            {
                portal.GetComponent<CloakPortalsActivator>().DisablePortals();
            }
            CanUsePortals = true;
            */
        }

        // PRIVATEs

            private IEnumerator DisableCanUsePortalForXSecondsCoroutine(float duration)
        {
            CanUsePortals = false;
            yield return new WaitForSeconds(duration);
            CanUsePortals = true;
        }


        private IEnumerator CloakDuration(float Duration)
        {
            yield return new WaitForSeconds(1);
            CanDisableCloak = true;
            SetCloack();
            yield return new WaitForSeconds(Duration);
            CanDisableCloak = false;
            UnsetCloack();
        }

        private void SendCloakEvent()
        {
            var cloakEvent = CloakAbilityEvent.Create();
            cloakEvent.ActivationBool = CanUseAbility;
            cloakEvent.Entity = entity;
            cloakEvent.Send();
        }

        private void SetCloack()
        {
            OnCloackSet.Invoke();
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_useCloakSound);
            KartMeshDisabler.DisableKartMeshes(true);
            CloakEffect.SetActive(true);

            if (entity.isOwner)
            {
                OnOwnerCloackSet.Invoke();
            }
        }

        private void UnsetCloack()
        {
            OnCloackUnset.Invoke();
            CanDisableCloak = false;
            CloakEffect.SetActive(false);
            MyExtensions.AudioExtensions.PlayClipObjectAndDestroy(_endCloakSound);
            KartMeshDisabler.EnableKartMeshes(true);

            if (entity.isAttached && entity.isOwner)
            {
                OnOwnerCloackUnset.Invoke();
            }
        }
    }
}
