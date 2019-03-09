using UnityEngine;

namespace Menu
{
    [CreateAssetMenu(menuName ="Menus/Next Game Option Colors")]
    public class NextGameOptionColors : ScriptableObject
    {
        public Color ChosenColor;
        public Color HighlightedColor;
        public Color NotChosenColor;
    }
}
