using Common;
using UnityEngine;

namespace Controls
{
    [DisallowMultipleComponent]
    public class KartInputManager : MonoBehaviour
    {
        public bool InGameInputsEnabled = true;
        
        [Header("References")]
        [SerializeField] private ControllableDisabler _controllableDisabler;

        public void DisableKartInputsInGame()
        {
            _controllableDisabler.StopAllCoroutines();
            _controllableDisabler.DisableAll();
            InGameInputsEnabled = false;
        }

        public void EnableKartInputsInGame()
        {
            _controllableDisabler.StopAllCoroutines();
            _controllableDisabler.EnableAll();
            InGameInputsEnabled = true;
        }

        public void DisableKartInputsForMenu()
        {
            _controllableDisabler.StopAllCoroutines();
            _controllableDisabler.DisableAll();
        }

        public void EnableKartInputsAfterMenu()
        {
            if (InGameInputsEnabled)
            {
                _controllableDisabler.StopAllCoroutines();
                _controllableDisabler.EnableAll();
            }
        }
    }
}