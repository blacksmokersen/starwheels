using System.Collections;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(MeshRenderer))]
    public class ItemBox : MonoBehaviour
    {
        public const float HideDuration = 2f;

        private BoxCollider boxCollider;
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
            meshRenderer = GetComponent<MeshRenderer>();
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
            meshRenderer.enabled = false;
        }

        private void Show()
        {
            boxCollider.enabled = true;
            meshRenderer.enabled = true;
        }
    }
}
