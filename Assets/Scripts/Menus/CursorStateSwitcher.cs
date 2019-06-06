using UnityEngine;

namespace Menu
{
    [DisallowMultipleComponent]
    public class CursorStateSwitcher : MonoBehaviour
    {
        // PUBLIC

        public void ShowCursor()
        {
            Cursor.visible = true;
        }

        public void HideCursor()
        {
            Cursor.visible = false;
        }
    }
}
