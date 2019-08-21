using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace Menu.Options
{
    public class GraphicsManipulations : MonoBehaviour
    {
        [Header("Graphics Quality")]
        private int _CurrentGraphicResolution;
        [SerializeField] private TextMeshProUGUI _qualityText;

        [Header("Resolution")]
        [SerializeField] private TextMeshProUGUI _resolutionText;
        private int _currentResolution;

        [Header("FullScreen")]
        [SerializeField] private TextMeshProUGUI _screenModeText;
        private bool _isFullScreen = true;

        [Header("Shadows Quality")]
        [SerializeField] private TextMeshProUGUI _shadowsResolutionText;
        private int _currentShadowsResolution;

        [Header("Anti Aliasing")]
        [SerializeField] private TextMeshProUGUI _antiAliasingText;
        private int[] _antiAliasingLevels = new int[4] { 0, 2, 4, 8 };
        [SerializeField] private string[] _antiAliasingNames = new string[4] { "Disabled", "2x Multi Sampling", "4x Multi Sampling", "8x Multi Sampling" };
        private int _currentAntiAliasing;

        [Header("Events")]
        public UnityEvent OnChangeEvent;

        private void Awake()
        {
            ResetParameters();
        }

        #region Quality
        public void InitializeQuality()
        {
            _CurrentGraphicResolution = QualitySettings.GetQualityLevel();
            SetQuality();
        }

        public void ChangeQuality(int value)
        {
            _CurrentGraphicResolution += value;

            if (_CurrentGraphicResolution >= QualitySettings.names.Length)
            {
                _CurrentGraphicResolution = 0;

            }
            else if (_CurrentGraphicResolution < 0)
            {
                _CurrentGraphicResolution = QualitySettings.names.Length - 1;
            }

            if (OnChangeEvent != null)
            {
                OnChangeEvent.Invoke();
            }

            SetQuality();
        }

        public void SetQuality()
        {
            if (_qualityText) // Displaying
            {
                _qualityText.text = QualitySettings.names[_CurrentGraphicResolution];
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
            if (OnChangeEvent != null)
            {
                OnChangeEvent.Invoke();
            }
            SetResolution();
        }

        public void SetResolution()
        {
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
            if (OnChangeEvent != null)
            {
                OnChangeEvent.Invoke();
            }

            SetFullScreen(!_isFullScreen);
        }
        #endregion

        #region ShadowsQuality
        public void InitializeShadowsResolution()
        {
            _currentShadowsResolution = (int)QualitySettings.shadowResolution;
            SetShadowsResolution();
        }

        public void ChangeShadowsResolution(int value)
        {
            _currentShadowsResolution += value;

            if (_currentShadowsResolution >= 4)
            {
                _currentShadowsResolution = 0;
            }
            else if (_currentShadowsResolution < 0)
            {
                _currentShadowsResolution = 3;
            }

            if (OnChangeEvent != null)
            {
                OnChangeEvent.Invoke();
            }

            SetShadowsResolution();
        }

        public void SetShadowsResolution()
        {
            if (_shadowsResolutionText) // Displaying
            {
                _shadowsResolutionText.text = ((ShadowResolution)_currentShadowsResolution).ToString();
            }
        }

        #endregion

        #region Alliasing
        public void InitializeAntiAliasing()
        {
            for (int i = 0; i < 4; i++)
            {
                if (_antiAliasingLevels[i] == QualitySettings.antiAliasing)
                {
                    _currentAntiAliasing = i;
                }
            }

            SetAntiAliasing();
        }

        public void ChangeAntiAliasing(int value)
        {
            _currentAntiAliasing += value;

            if (_currentAntiAliasing >= 4)
            {
                _currentAntiAliasing = 0;
            }
            else if (_currentAntiAliasing < 0)
            {
                _currentAntiAliasing = 3;
            }

            if (OnChangeEvent != null)
            {
                OnChangeEvent.Invoke();
            }

            SetAntiAliasing();
        }

        public void SetAntiAliasing()
        {
            if (_antiAliasingText) // Displaying
            {
                _antiAliasingText.text = _antiAliasingNames[_currentAntiAliasing];
            }
        }
        #endregion

        public void Validation()
        {
            QualitySettings.SetQualityLevel(_CurrentGraphicResolution);

            if (Screen.resolutions[_currentResolution].height != Screen.currentResolution.height && Screen.resolutions[_currentResolution].width != Screen.currentResolution.width)
            {
                Screen.SetResolution(Screen.resolutions[_currentResolution].width, Screen.resolutions[_currentResolution].height, _isFullScreen);
            }

            if (_isFullScreen != Screen.fullScreen)
            {
                Screen.fullScreen = _isFullScreen;
            }

            if (_currentShadowsResolution != (int)QualitySettings.shadowResolution)
            {
                QualitySettings.shadowResolution = ((ShadowResolution)_currentShadowsResolution);
            }

            if (_antiAliasingLevels[_currentAntiAliasing] != QualitySettings.antiAliasing)
            {
                QualitySettings.antiAliasing = _antiAliasingLevels[_currentAntiAliasing];
            }
        }

        public void ResetParameters()
        {
            InitializeQuality();
            InitializeScreenMode();
            InitializeResolutions();
            InitializeShadowsResolution();
            InitializeAntiAliasing();
        }
    }
}
