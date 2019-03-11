using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Menu
{
    public class NextGameOption : MonoBehaviour
    {
        [Header("Settings")]
        public NextGameOptionEntrySettings Settings;
        [SerializeField] private NextGameOptionColors ColorSettings;

        [Header("UI Elements")]
        [SerializeField] private HorizontalLayoutGroup _choicesLayout;
        [SerializeField] private Image _thisChoiceBackground;
        [SerializeField] private TextMeshProUGUI _optionText;

        [Header("Events")]
        [Tooltip("Value is between 0 and 1 for the completion percentage of this option.")]
        public FloatEvent OnEntryTimeUpdate;
        public StringEvent OnOptionChosen;

        private float timer = 0f;
        private Toggle[] _toggles;

        // CORE

        private void Awake()
        {
            _thisChoiceBackground.color = ColorSettings.NotChosenColor;
        }

        private void OnEnable()
        {
            if (Settings.IsFirstOptionInMenu)
            {
                SetActive();
            }
        }

        // PUBLIC

        public void SetActive()
        {
            InitializeListeners();
            _thisChoiceBackground.color = ColorSettings.HighlightedColor;
            StartCoroutine(UpdateTimeRoutine());
        }

        public void SetChoice(string value)
        {
            Settings.Choice = value;
            _thisChoiceBackground.color = ColorSettings.ChosenColor;
            OnOptionChosen.Invoke(Settings.Choice);
        }

        // PRIVATE

        private void InitializeListeners()
        {
            if (_choicesLayout)
            {
                _choicesLayout.gameObject.SetActive(true);
                _toggles = _choicesLayout.GetComponentsInChildren<Toggle>();

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
            if (_choicesLayout)
            {
                _choicesLayout.gameObject.SetActive(false);
            }
            StopAllCoroutines();
        }
    }
}
