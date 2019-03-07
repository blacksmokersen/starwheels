using UnityEngine;

namespace Menu
{
    public class NextGameOptionEntrySettings : ScriptableObject
    {
        public StringVariable OptionName;
        public float SecondsBeforeNextOption;
        public string Choice;
    }
}
