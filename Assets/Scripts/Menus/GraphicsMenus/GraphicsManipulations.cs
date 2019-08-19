using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Menu.Options
{
    public class GraphicsManipulations : MonoBehaviour
    {
        [Header("Graphics Quality")]
        private int _CurrentGraphicQuality;
        [SerializeField] private TextMeshProUGUI _qualityText;

        [Header("Resolution")]
        [SerializeField] private TextMeshProUGUI _resolutionText;
        private int _currentResolution;

        [Header("FullScreen")]
        [SerializeField] private TextMeshProUGUI _screenModeText;
        private bool _isFullScreen;

        private void Awake()
        {
            InitializeQuality();
            InitializeScreenMode();
            InitializeResolutions();
        }

        #region Quality
        public void InitializeQuality()
        {
            _CurrentGraphicQuality = QualitySettings.GetQualityLevel();
            SetQuality();
        }

        public void ChangeQuality(int value)
        {
            _CurrentGraphicQuality += value;

            if (_CurrentGraphicQuality >= QualitySettings.names.Length)
            {
                _CurrentGraphicQuality = 0;

            }
            else if (_CurrentGraphicQuality < 0)
            {
                _CurrentGraphicQuality = QualitySettings.names.Length - 1;
            }

            SetQuality();
        }

        public void SetQuality()
        {
            QualitySettings.SetQualityLevel(_CurrentGraphicQuality);
            if (_qualityText) // Displaying
            {
                _qualityText.text = QualitySettings.names[_CurrentGraphicQuality];
            }
        }
        #endregion

        #region Resolutions

        public void InitializeResolutions()
        {
            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                if (Screen.resolutions[i].height == Screen.currentResolution.height && Screen.resolutions[i].width == Screen.currentResolution.width)
                {
                    Debug.Log("FindedResolution");
                    _currentResolution = i;
                    i = Screen.resolutions.Length;
                }
            }

            SetResolution();
        }

        public void ChangeResolution(int value)
        {
            _currentResolution += value;

            if (_currentResolution >= Screen.resolutions.Length)
            {
                _currentResolution = 0;

            }
            else if (_currentResolution < 0)
            {
                _currentResolution = Screen.resolutions.Length - 1;
            }
            SetResolution();
        }

        public void SetResolution()
        {
            if (Screen.resolutions[_currentResolution].height != Screen.currentResolution.height && Screen.resolutions[_currentResolution].width != Screen.currentResolution.width)
            {
                Screen.SetResolution(Screen.resolutions[_currentResolution].width, Screen.resolutions[_currentResolution].height, _isFullScreen);
            }

            if (_resolutionText)
            {
                _resolutionText.text = Screen.resolutions[_currentResolution].width + " " + Screen.resolutions[_currentResolution].height;
            }
        }

        #endregion

        #region Fullscreen

        public void InitializeScreenMode()
        {
            SetFullScreen(Screen.fullScreen);
        }

        public void SetFullScreen(bool value)
        {
            _isFullScreen = value;
            if (_isFullScreen != Screen.fullScreen)
            {
                Screen.fullScreen = value;
            }

            if (_screenModeText)
            {
                if (_isFullScreen)
                {
                    _screenModeText.text = "Full Screen";

                }
                else
                {
                    _screenModeText.text = "Windowed";
                }
            }
        }

        public void ChangeScreenMode()
        {
            SetFullScreen(!_isFullScreen);
        }
        #endregion
    }
}
