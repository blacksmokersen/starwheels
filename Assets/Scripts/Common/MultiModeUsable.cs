using UnityEngine;

namespace Common
{
    [DisallowMultipleComponent]
    public class MultiModeUsable : MonoBehaviour
    {
        public int Mode;

        [Header("Events")]
        public IntEvent OnModeUpdated;

        public void SetMode(int mode)
        {
            Mode = mode;
            OnModeUpdated.Invoke(Mode);
        }
    }
}
