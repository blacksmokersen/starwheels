using UnityEngine;
using Multiplayer;

namespace Abilities
{
    public class AbilitySetter : MonoBehaviour
    {
        [SerializeField] private GameObject _jumpingAbility;
        [SerializeField] private GameObject _tpBackAbility;
        [SerializeField] private GameObject _cloakAbility;

        private GameObject[] _abilities;
        private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
            _abilities = new GameObject[3] { _jumpingAbility, _tpBackAbility, _cloakAbility };
            SetAbility(_playerSettings.AbilityIndex);
        }

        // PUBLIC

        public void SetAbility(int index)
        {
            _playerSettings.AbilityIndex = index;

            for (int i = 0; i < _abilities.Length; i++)
            {
                if (i == _playerSettings.AbilityIndex)
                {
                    _abilities[i].SetActive(true);
                }
                else
                {
                    _abilities[i].SetActive(false);
                }
            }
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
    }
}
