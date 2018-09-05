using System.Collections;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(BoxCollider))]
    public class ItemBox : MonoBehaviour
    {
        public GameObject itemSphere;

        public const float HideDuration = 2f;
        private BoxCollider boxCollider;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        public IEnumerator Activate()
        {
            Hide();
            yield return new WaitForSeconds(HideDuration);
            Show();
        }

        private void Hide()
        {
            boxCollider.enabled = false;
            itemSphere.SetActive(false);
        }

        private void Show()
        {
            boxCollider.enabled = true;
            itemSphere.SetActive(true);
        }
    }
}
