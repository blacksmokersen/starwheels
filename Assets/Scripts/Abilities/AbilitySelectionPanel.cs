using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Multiplayer;
using TMPro;
using Bolt;
using SW.Matchmaking;

namespace Abilities
{
    public class AbilitySelectionPanel : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private IntVariable _secondsBeforeAutocomplete;

        [Header("UI Elements")]
        [SerializeField] private GameObject _abilitySelectionPanelRootObject;
        [SerializeField] private TextMeshProUGUI _secondsLeftText;

        private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
            InitializeListeners();
            ShowPanel();
        }

        // PUBLIC

        public void ShowPanel()
        {
            _abilitySelectionPanelRootObject.SetActive(true);
            StartCoroutine(HidePanelAfterXSecondsRoutine());
        }

        private void HidePanel()
        {
            _abilitySelectionPanelRootObject.SetActive(false);
            StopAllCoroutines();
        }

        // PRIVATE

        private void InitializeListeners()
        {
            var abilityToggles = GetComponentsInChildren<Toggle>();
            foreach (var toggle in abilityToggles)
            {
                var abilityIndex = toggle.GetComponentInChildren<AbilitySelectionPanelLabel>().AbilityIndex.Value;

                if (toggle.isOn)
                {
                    _playerSettings.AbilityIndex = abilityIndex;
                    _playerSettings.OnAbilityIndexUpdated.Invoke();

                    PlayerStatUpdate playerAbilityUpdate = PlayerStatUpdate.Create();
                    playerAbilityUpdate.StatName = Constants.PlayerStats.Ability;
                    playerAbilityUpdate.PlayerID = SWMatchmaking.GetMyBoltId();
                    playerAbilityUpdate.StatValue = _playerSettings.AbilityIndex;
                    playerAbilityUpdate.Send();
                }

                toggle.onValueChanged.AddListener((b) => UpdateAbilityIndex(abilityIndex, b));
            }
        }

        private void UpdateAbilityIndex(int abilityIndex, bool toggleIsOn)
        {
            if (toggleIsOn)
            {
                _playerSettings.AbilityIndex = abilityIndex;
                _playerSettings.OnAbilityIndexUpdated.Invoke();

                PlayerStatUpdate playerAbilityUpdate = PlayerStatUpdate.Create();
                playerAbilityUpdate.StatName = Constants.PlayerStats.Ability;
                playerAbilityUpdate.PlayerID = SWMatchmaking.GetMyBoltId();
                playerAbilityUpdate.StatValue = _playerSettings.AbilityIndex;
                playerAbilityUpdate.Send();
            }

            HidePanel();
        }

        private void UpdateTimeRemaining(int secondsRemaining)
        {
            if (secondsRemaining > 0)
            {
                _secondsLeftText.text = secondsRemaining + "S REMAINING";
            }
            else
            {
                _secondsLeftText.text = "";
            }
        }

        private IEnumerator HidePanelAfterXSecondsRoutine()
        {
            var timeRemaining = _secondsBeforeAutocomplete.Value;
            while (timeRemaining > 0)
            {
                UpdateTimeRemaining(timeRemaining);
                yield return new WaitForSeconds(1);
                timeRemaining--;
            }
            UpdateTimeRemaining(0);
            HidePanel();
        }
    }
}
