using UnityEngine;

namespace SW.Customization
{
    [DisallowMultipleComponent]
    public class KartSkinSettings : MonoBehaviour
    {
        [Header("Settings")]
        public int Index;
        public string KartName;
        public string PassiveDescription;
        public Renderer TargetRenderer;
    }
}
