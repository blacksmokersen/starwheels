using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    [DisallowMultipleComponent]
    public class ToggleImageSwitcher : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Toggle _targetToggle;
        [Space(15)]
        [SerializeField] private Image _unselectedImage;
        [SerializeField] private Image _onHoverImage;
        [SerializeField] private Image _onHoverValidationInputImage;
        [SerializeField] private Image _selectedImage;

        // CORE

        private void Awake()
        {
            SetUnselectedImage();
        }

        // PUBLIC

        public void SetUnselectedImage()
        {
            if (!_targetToggle.isOn)
            {
                _unselectedImage.gameObject.SetActive(true);
                _onHoverImage.gameObject.SetActive(false);
                _onHoverValidationInputImage.gameObject.SetActive(false);
                _selectedImage.gameObject.SetActive(false);
            }
        }

        public void SetHoverImage()
        {
            if (!_targetToggle.isOn)
            {
                _unselectedImage.gameObject.SetActive(false);
                _onHoverImage.gameObject.SetActive(true);
                _onHoverValidationInputImage.gameObject.SetActive(true);
                _selectedImage.gameObject.SetActive(false);
            }
        }

        public void SetSelectedImage()
        {
            _unselectedImage.gameObject.SetActive(false);
            _onHoverImage.gameObject.SetActive(false);
            _onHoverValidationInputImage.gameObject.SetActive(false);
            _selectedImage.gameObject.SetActive(true);
        }
    }
}
