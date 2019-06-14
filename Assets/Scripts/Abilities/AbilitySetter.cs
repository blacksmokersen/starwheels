using UnityEngine;
using Multiplayer;

namespace Abilities
{
    [DisallowMultipleComponent]
    public class AbilitySetter : MonoBehaviour
    {
        [SerializeField] private GameObject _wallAbility;
        [SerializeField] private GameObject _jumpingAbility;
        [SerializeField] private GameObject _tpBackAbility;

        private GameObject[] _abilities;
        private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
            _playerSettings.OnAbilityIndexUpdated.AddListener(UpdateAbility);

            _abilities = new GameObject[3] { _wallAbility , _jumpingAbility, _tpBackAbility, };
            SetAbility(_playerSettings.AbilityIndex);
        }

        // PUBLIC

        public void SetAbility(int index)
        {
            _playerSettings.AbilityIndex = index;

            for (int i = 0; i < _abilities.Length; i++)
            {
                if (_abilities[i] != null)
                {
                    _abilities[i].SetActive(i == index);
                }
            }
        }

        public void UpdateAbility()
        {
            SetAbility(_playerSettings.AbilityIndex);
        }

        public Ability GetCurrentAbility()
        {
            var ability = _abilities[_playerSettings.AbilityIndex].GetComponent<Ability>();
            if (ability)
                return ability;
            else
                Debug.LogError("Could not find current ability.");
            return null;
        }

        public void SetAbilityIndex(int index)
        {
            _playerSettings.AbilityIndex = index;
        }

        public void SetNextAbilityIndex()
        {
            _playerSettings.AbilityIndex = (_playerSettings.AbilityIndex + 1) % 3;
        }

        public void SetPreviousAbilityIndex()
        {
            _playerSettings.AbilityIndex = (_playerSettings.AbilityIndex + 2) % 3;
        }
    }
}
