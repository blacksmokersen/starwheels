using System.Collections;
using UnityEngine;

namespace Items.Lottery
{
    [RequireComponent(typeof(BoxCollider))]
    public class ItemBox : MonoBehaviour
    {
        [SerializeField] private GameObject itemSphere;
        [SerializeField] private float cooldown = 2f;

        [Header("Colors")]
        [SerializeField] private ParticleSystem _sphere;
        [SerializeField] private ParticleSystem _sphereCenter;
        [SerializeField] private ParticleSystem _sphereSurroundings;
        [SerializeField] private Light _centerLight;

        [Header("Settings")]
        public ItemBoxSettings CurrentSettings;
        [SerializeField] private ItemBoxSettings _firstSettings;

        private BoxCollider _boxCollider;
        private Coroutine _upgradeCoroutine;

        // CORE

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();

            UpgradeSelf(_firstSettings);
        }

        private void Start()
        {
            _upgradeCoroutine = StartCoroutine(UpgradeSelfRoutine());
        }

        // PUBLIC

        public void Activate()
        {
            StartCoroutine(StartCooldown());
        }

        public IEnumerator StartCooldown()
        {
            Hide();
            yield return new WaitForSeconds(cooldown);
            Show();
        }

        // PRIVATE

        private void Hide()
        {
            _boxCollider.enabled = false;
            itemSphere.SetActive(false);

            if (_upgradeCoroutine != null)
            {
                StopCoroutine(_upgradeCoroutine);
            }
            CurrentSettings = _firstSettings;
        }

        private void Show()
        {
            _boxCollider.enabled = true;
            itemSphere.SetActive(true);

            _upgradeCoroutine = StartCoroutine(UpgradeSelfRoutine());
        }

        private void UpgradeSelf(ItemBoxSettings settings)
        {
            var sphereMain = _sphere.main;
            sphereMain.startColor = settings.SphereColor;
            var sphereSurroundingsMain = _sphereSurroundings.main;
            sphereSurroundingsMain.startColor = settings.SphereSurroundingParticlesColor;
            var sphereCenterMain = _sphereCenter.main;
            sphereCenterMain.startColor = settings.SphereCenterColor;
            _centerLight.color = settings.CenterLightColor;

            CurrentSettings = settings;
        }

        private IEnumerator UpgradeSelfRoutine()
        {
            yield return new WaitForSeconds(CurrentSettings.SecondsBeforeUpgrade);

            if (CurrentSettings.NextUpgradeSettings)
            {
                UpgradeSelf(CurrentSettings.NextUpgradeSettings);
                StartCoroutine(UpgradeSelfRoutine());
            }
        }

        private void ChangeParticleSystemColor(ParticleSystem particleSystem, Color color)
        {

        }
    }
}
