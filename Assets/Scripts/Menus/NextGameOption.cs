using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Bolt;

namespace Menu
{
    [System.Serializable]
    struct ChoiceMap
    {
        public string ChoiceName;
        public HorizontalLayoutGroup ChoicePanel;
    }

    public class NextGameOption : GlobalEventListener
    {
        [Header("Settings")]
        public NextGameOptionEntrySettings Settings;
        [SerializeField] private NextGameOptionColors ColorSettings;

        [Header("UI Elements")]
        [SerializeField] private List<ChoiceMap> _choicesMap;
        [SerializeField] private Image _thisChoiceBackground;
        [SerializeField] private TextMeshProUGUI _optionText;

        [Header("Events")]
        [Tooltip("Value is between 0 and 1 for the completion percentage of this option.")]
        public FloatEvent OnEntryTimeUpdate;
        public StringEvent OnOptionChosen;

        private HorizontalLayoutGroup _activePanel;
        private float timer = 0f;
        private Toggle[] _toggles;

        // CORE

        private void Awake()
        {
            _thisChoiceBackground.color = ColorSettings.NotChosenColor;
        }

        private new void OnEnable()
        {
            base.OnEnable();
            if (Settings.IsFirstOptionInMenu)
            {
                SetActive();
            }
        }

        // BOLT

        public override void OnEvent(NextGameOptionUpdate evnt)
        {
            if (BoltNetwork.IsClient && evnt.OptionName == Settings.OptionName)
            {
                SetChoice(evnt.NewChoice);
                HideChoices();
            }
        }

        // PUBLIC

        public void SetActive()
        {
            if (_choicesMap.Count > 0)
            {
                _activePanel = _choicesMap[0].ChoicePanel;
                InitializeListeners(_activePanel);
            }
            _thisChoiceBackground.color = ColorSettings.HighlightedColor;
            StartCoroutine(UpdateTimeRoutine());
        }

        public void SetActiveDynamic(string previousChoice)
        {
            foreach (var choice in _choicesMap)
            {
                if (previousChoice == choice.ChoiceName)
                {
                    _activePanel = choice.ChoicePanel;
                    InitializeListeners(_activePanel);
                    break;
                }
            }
            _thisChoiceBackground.color = ColorSettings.HighlightedColor;
            StartCoroutine(UpdateTimeRoutine());
        }

        public void SetChoice(string choice)
        {
            Settings.Choice = choice;
            SetChoiceText(choice);
            _thisChoiceBackground.color = ColorSettings.ChosenColor;

            if (BoltNetwork.IsServer)
            {
                NextGameOptionUpdate nextGameOptionUpdateEvent = NextGameOptionUpdate.Create();
                nextGameOptionUpdateEvent.OptionName = Settings.OptionName;
                nextGameOptionUpdateEvent.NewChoice = choice;
                nextGameOptionUpdateEvent.Send();
            }

            OnOptionChosen.Invoke(Settings.Choice);
        }

        public void SetChoiceText(string choice)
        {
            _optionText.text = Settings.OptionName + " : " + choice;
        }

        // PRIVATE

        private void InitializeListeners(HorizontalLayoutGroup choicesLayout)
        {
            if (BoltNetwork.IsServer)
            {
                choicesLayout.gameObject.SetActive(true);
                _toggles = choicesLayout.GetComponentsInChildren<Toggle>();

                foreach (var toggle in _toggles)
                {
                    var choice = toggle.GetComponentInChildren<Label>().String.Value;
                    toggle.onValueChanged.AddListener((b) =>
                    {
                        SetChoice(choice);
                        HideChoices();
                    });
                }
            }
        }

        private void SelectRandomChoice()
        {
            if (_toggles != null)
            {
                SetChoice(_toggles[Random.Range(0, _toggles.Length)].GetComponentInChildren<Label>().String.Value);
            }
            else
            {
                SetChoice("default");
            }
        }

        private void UpdateTimer(float secondsLeft)
        {
            _optionText.text = Settings.OptionName + " : " + secondsLeft;
        }

        private IEnumerator UpdateTimeRoutine()
        {
            while (timer < Settings.SecondsBeforeNextOption)
            {
                var delta = Time.deltaTime;
                timer += delta;
                UpdateTimer((int)(Settings.SecondsBeforeNextOption - timer));
                OnEntryTimeUpdate.Invoke(timer / Settings.SecondsBeforeNextOption);
                yield return new WaitForEndOfFrame();
            }
            OnEntryTimeUpdate.Invoke(1f);

            SelectRandomChoice();
            HideChoices();
        }

        private void HideChoices()
        {
            if (_activePanel)
            {
                _activePanel.gameObject.SetActive(false);
            }
            StopAllCoroutines();
        }
    }
}
