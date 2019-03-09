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
            _choicesLayout.gameObject.SetActive(true);
            var toggles = _choicesLayout.GetComponentsInChildren<Toggle>();

            foreach (var toggle in toggles)
            {
                var choice = toggle.GetComponent<Label>().String.Value;
                toggle.onValueChanged.AddListener((b) =>
                {
                    SetChoice(choice);
                    HideChoices();
                });
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
            HideChoices();
        }

        private void HideChoices()
        {
            _choicesLayout.gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }
}
