using UnityEngine;

namespace Menu
{
    [CreateAssetMenu(menuName = "Menus/Next Game Option Entry")]
    public class NextGameOptionEntrySettings : ScriptableObject
    {
        public string OptionName;
        public bool IsFirstOptionInMenu;
        public float SecondsBeforeNextOption;
        public string Choice;
    }
}
