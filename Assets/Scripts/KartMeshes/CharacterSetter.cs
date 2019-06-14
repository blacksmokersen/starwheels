using UnityEngine;
using Bolt;
using Multiplayer;

namespace SW.Customization
{
    [DisallowMultipleComponent]
    public class CharacterSetter : GlobalEventListener
    {
        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;

        [Header("Meshes")]
        [SerializeField] private GameObject _character0;
        [SerializeField] private GameObject _character1;
        [SerializeField] private GameObject _character2;

        private GameObject[] _characters;

        // CORE

        private void Awake()
        {
            _characters = new GameObject[3] { _character0, _character1, _character2 };
        }

        // BOLT

        public override void OnEvent(PlayerReady evnt)
        {
            SetCharacter(1);
        }

        // PUBLIC

        public void SetCharacter(int index)
        {
            for (int i = 0; i < _characters.Length; i++)
            {
                if (i == index)
                {
                    _characters[i].SetActive(true);
                }
                else
                {
                    _characters[i].SetActive(false);
                }
            }
        }
    }
}
