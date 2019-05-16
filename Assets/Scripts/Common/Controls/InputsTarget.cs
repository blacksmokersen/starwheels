using UnityEngine;

namespace Common.Controls
{
    [DisallowMultipleComponent]
    public class InputsTarget : MonoBehaviour
    {
        public bool HasFocus = false;

        [Tooltip("Optional Controllable Disabler to disable specific controls.")]
        [SerializeField] private ControllableDisabler _controllableDisabler;
        [SerializeField] private bool _targetChildren;

        private InputsTarget[] _allTargets;

        public void DisableAllTargets()
        {
            if (_allTargets == null)
            {
                _allTargets = FindObjectsOfType<InputsTarget>();
            }
            foreach (var target in _allTargets)
            {
                target.Disable();
            }
        }

        public void EnableAllTargets()
        {
            if (_allTargets == null)
            {
                _allTargets = FindObjectsOfType<InputsTarget>();
            }
            foreach (var target in _allTargets)
            {
                target.Enable();
            }
        }

        public void Disable()
        {
            HasFocus = true;

            if (_controllableDisabler)
            {
                if (_targetChildren)
                {
                    _controllableDisabler.DisableAllInChildren();
                }
                else
                {
                    _controllableDisabler.DisableAll();
                }
            }
        }

        public void Enable()
        {
            HasFocus = false;

            if (_controllableDisabler)
            {
                if (_targetChildren)
                {
                    _controllableDisabler.EnableAllInChildren();
                }
                else
                {
                    _controllableDisabler.EnableAll();
                }
            }
        }
    }
}
