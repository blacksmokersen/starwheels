using UnityEngine;
using Common;
using Gamemodes;
using SWExtensions;

namespace Controls
{
    public class KartDisabler : MonoBehaviour
    {
        public bool HasInGameControl = true;
        
        [Header("Settings")]
        [SerializeField] private bool _disableKartOnEnabled;
        [SerializeField] private bool _enableKartOnDisabled;
        
        [Header("References")]
        [SerializeField] private BoolVariable _gameStarted;

        private KartInputManager _kartInputManager;

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
            if (_kartInputManager == null)
            {
                FindInputManager();
            }
            if (_kartInputManager && _gameStarted.Value && HasInGameControl)
            {
                _kartInputManager.EnableKartInputsAfterMenu();
            }
        }

        public void Disable()
        {
            if (_kartInputManager == null)
            {
                FindInputManager();
            }
            if (_kartInputManager && _gameStarted.Value)
            {
                _kartInputManager.DisableKartInputsForMenu();
            }
        }

        // PRIVATE

        private void FindInputManager()
        {
            var myKart = KartExtensions.GetMyKart();
            if (myKart)
            {
                _kartInputManager = myKart.GetComponentInChildren<KartInputManager>();
            }
        }
    }
}
