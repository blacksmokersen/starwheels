using UnityEngine;
using Multiplayer;

namespace SW.Abilities
{
    [DisallowMultipleComponent]
    public class AbilityMenuSetter : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;

        [Header("Previews")]
        [SerializeField] private GameObject _wallAbilityPreview;
        [SerializeField] private string _wallAbilityDescription;
        [SerializeField] private GameObject _jumpingAbilityPreview;
        [SerializeField] private string _jumpAbilityDescription;
        [SerializeField] private GameObject _tpBackAbilityPreview;
        [SerializeField] private string _tpBackAbilityDescription;

        private GameObject[] _abilityPreviews;
        private string[] _abilityDescriptions;

        // CORE

        private void Awake()
        {
            _abilityPreviews = new GameObject[3] { _wallAbilityPreview, _jumpingAbilityPreview, _tpBackAbilityPreview };
            _abilityDescriptions = new string[3] { _wallAbilityDescription, _jumpAbilityDescription, _tpBackAbilityDescription };

            InitializeIndex();
        }

        // PUBLIC

        public string GetCurrentAbilityDescription()
        {
            return _abilityDescriptions[_playerSettings.AbilityIndex];
        }

        public void SetAbilityIndexAndPreview(int index)
        {
            _playerSettings.AbilityIndex = index;
            SetAbilityPreview(index);
        }

        public void SetAbilityPreview(int index)
        {
            for (int i = 0; i < _abilityPreviews.Length; i++)
            {
                if (_abilityPreviews[i] != null)
                {
                    _abilityPreviews[i].SetActive(i == index);
                }
            }
        }

        public void SetNextAbility()
        {
            SetAbilityIndexAndPreview((_playerSettings.AbilityIndex + 1) % 3);
        }

        public void SetPreviousAbility()
        {
            SetAbilityIndexAndPreview((_playerSettings.AbilityIndex + 2) % 3);
        }

        // PRIVATE

        private void InitializeIndex()
        {
            for (int i = 0; i < _abilityPreviews.Length; i++)
            {
                if (_abilityPreviews[i].activeInHierarchy)
                {
                    _playerSettings.AbilityIndex = i;
                }
            }
        }
    }
}
