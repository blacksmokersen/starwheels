using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Toggle))]
    public class ToggleResetOnDisable : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Toggle>().isOn = false;
        }
    }
}
