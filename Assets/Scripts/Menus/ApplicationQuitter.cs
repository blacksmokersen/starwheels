using UnityEngine;

namespace Menu
{
    [DisallowMultipleComponent]
    public class ApplicationQuitter : MonoBehaviour
    {
        // PUBLIC

        public void Quit()
        {
            Application.Quit();
        }
    }
}
