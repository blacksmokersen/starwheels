using UnityEngine;

namespace Menu
{
    public class HideIfClickedOutside : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)
                && gameObject.activeSelf
                && !RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(), Input.mousePosition, null))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
