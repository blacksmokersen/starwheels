using UnityEngine;
using UnityEngine.Events;

namespace Menu
{
    public class HideIfClickedOutside : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent OnClickedOutside;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)
                && gameObject.activeSelf
                && !RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(), Input.mousePosition, null))
            {
                gameObject.SetActive(false);

                OnClickedOutside.Invoke();
            }
        }
    }
}
