using UnityEngine;
using Common;
using SWExtensions;

namespace Controls
{
    public class KartDisabler : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool _disableKartOnEnabled;
        [SerializeField] private bool _enableKartOnDisabled;
        [SerializeField] private BoolVariable _gameStarted;

        private ControllableDisabler _disabler;

        // CORE

        private void OnEnable()
        {
            if (_disableKartOnEnabled)
            {
                Disable();
            }
        }

        private void OnDisable()
        {
            if (_enableKartOnDisabled)
            {
                Enable();
            }
        }

        // PUBLIC

        public void Enable()
        {
            if (_disabler == null)
            {
                FindDisabler();
            }
            if (_disabler && _gameStarted.Value)
            {
                _disabler.EnableAllInChildren();
            }
        }

        public void Disable()
        {
            if (_disabler == null)
            {
                FindDisabler();
            }
            if (_disabler && _gameStarted.Value)
            {
                _disabler.DisableAllInChildren();
            }
        }

        // PRIVATE

        private void FindDisabler()
        {
            var myKart = KartExtensions.GetMyKart();
            if (myKart)
            {
                _disabler = myKart.GetComponent<ControllableDisabler>();
            }
        }
    }
}
