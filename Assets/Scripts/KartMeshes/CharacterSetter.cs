using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Multiplayer;

namespace SW.Customization
{
    [DisallowMultipleComponent]
    public class CharacterSetter : GlobalEventListener
    {
        [Header("Events")]
        public UnityEvent OnCharacterSwitched;

        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;

        [Header("Characters")]
        public GameObject CurrentCharacter;
        public Animator CurrentCharacterAnimator;
        [SerializeField] private GameObject _character0;
        [SerializeField] private GameObject _character1;
        [SerializeField] private GameObject _character2;

        private GameObject[] _characters;
        private int _currentIndex;

        // CORE

        private void Awake()
        {
            _characters = new GameObject[3] { _character0, _character1, _character2 };
        }

        private void Start()
        {
            SetCharacterWithLocalSettings();
        }

        // BOLT

        public override void OnEvent(PlayerReady evnt)
        {
            if (!evnt.Entity.IsOwner && evnt.Entity == GetComponentInParent<BoltEntity>())
            {
                SetCharacter(evnt.CharacterIndex);
            }
        }

        // PUBLIC

        public void SetCharacter(int index)
        {
            for (int i = 0; i < _characters.Length; i++)
            {
                if (i == index)
                {
                    CurrentCharacter = _characters[i];
                    CurrentCharacter.SetActive(true);
                    CurrentCharacterAnimator = CurrentCharacter.GetComponent<Animator>();

                    _currentIndex = i;
                    _playerSettings.CharacterIndex = i;

                    if (OnCharacterSwitched != null)
                    {
                        OnCharacterSwitched.Invoke();
                    }
                }
                else
                {
                    _characters[i].SetActive(false);
                }
            }
        }

        public void SetNextCharacter()
        {
            SetCharacter((_currentIndex + 1) % _characters.Length);
        }

        public void SetPreviousCharacter()
        {
            SetCharacter((_currentIndex - 1 + _characters.Length) % _characters.Length);
        }

        public void SetCharacterWithLocalSettings()
        {
            SetCharacter(_playerSettings.CharacterIndex);
        }
    }
}
