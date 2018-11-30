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

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);

            _abilities = new GameObject[3] { _jumpingAbility, _tpBackAbility, _cloakAbility };

            for(int i = 0; i < _abilities.Length; i++)
            {
                if (i == _playerSettings.AbilityIndex)
                    _abilities[i].SetActive(true);
                else
                    _abilities[i].SetActive(false);
            }
        }

        public void SetJumpingAbility()
        {
            foreach (var ability in _abilities)
                ability.SetActive(false);
            _jumpingAbility.SetActive(true);
        }

        public void SetTPBackAbility()
        {
            foreach (var ability in _abilities)
                ability.SetActive(false);
            _tpBackAbility.SetActive(true);
        }

        public void SetCloakAbility()
        {
            foreach (var ability in _abilities)
                ability.SetActive(false);
            _cloakAbility.SetActive(true);
        }
    }
}
