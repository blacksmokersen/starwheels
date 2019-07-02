using UnityEngine;
using Multiplayer;

namespace SW.Abilities
{
    [DisallowMultipleComponent]
    public class AbilityHUDLogoUpdater : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;

        [Header("UI Elements")]
        [SerializeField] private GameObject _wallLogo;
        [SerializeField] private GameObject _jumpLogo;
        [SerializeField] private GameObject _rewinderLogo;

        private GameObject[] _abilityLogos;

        // CORE

        private void Awake()
        {
            _abilityLogos = new GameObject[3] { _wallLogo, _jumpLogo, _rewinderLogo };

            SetAbilityLogo(_playerSettings.AbilityIndex);
            _playerSettings.OnAbilityIndexUpdated.AddListener(UpdateAbilityLogoWithSettings);
        }

        // PUBLIC

        public void SetAbilityLogo(int index)
        {
            for (int i = 0; i < _abilityLogos.Length; i++)
            {
                if (_abilityLogos[i] != null)
                {
                    _abilityLogos[i].SetActive(i == index);
                }
            }
        }

        public void UpdateAbilityLogoWithSettings()
        {
            SetAbilityLogo(_playerSettings.AbilityIndex);
        }
    }
}
