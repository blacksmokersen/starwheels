using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Menu.Options
{
    [DisallowMultipleComponent]
    public class GraphicsPanelSettings : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Toggle _fullScreenToggle;
        [SerializeField] private TMP_Dropdown _resolutionDropdown;

        private const string _fullScreenPrefName = "FullScreenPref";
        private const string _resolutionPrefName = "ScreenResolutionPref";

        // CORE

        private void Awake()
        {
            InitializeUI();
        }

        private void OnEnable()
        {
            InitializeUI();
        }

        private void OnDisable()
        {
            PlayerPrefs.Save(); // Save values when the panel closes
        }

        // PUBLIC

        public void SetFullScreen(bool b)
        {
            Screen.fullScreen = b;
            PlayerPrefs.SetInt(_fullScreenPrefName, b ? 1 : 0);
        }

        public void SetScreenResolution(int value)
        {
            switch (value)
            {
                case 0:
                    Screen.SetResolution(1920, 1080, Screen.fullScreen);
                    break;
                case 1:
                    Screen.SetResolution(1366, 768, Screen.fullScreen);
                    break;
                case 2:
                    Screen.SetResolution(1280, 720, Screen.fullScreen);
                    break;
                default:
                    break;
            }
            PlayerPrefs.SetInt(_resolutionPrefName, value);
        }

        public void SetGraphicQuality(int value)
        {
            switch (value)
            {
                case 0:
                    QualitySettings.SetQualityLevel(0);
                    break;
                case 1:
                    QualitySettings.SetQualityLevel(1);
                    break;
                case 2:
                    QualitySettings.SetQualityLevel(2);
                    break;
                default:
                    break;
            }
        }

        // PRIVATE

        private void InitializeUI()
        {
            _fullScreenToggle.isOn = PlayerPrefs.GetInt(_fullScreenPrefName) == 1 ? true : false;
            _resolutionDropdown.value = PlayerPrefs.GetInt(_resolutionPrefName);
        }
    }
}
