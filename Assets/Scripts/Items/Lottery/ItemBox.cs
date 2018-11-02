using System.Collections;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(BoxCollider))]
    public class ItemBox : MonoBehaviour
    {
        [SerializeField] private GameObject itemSphere;
        [SerializeField] private float cooldown = 2f;

        private BoxCollider boxCollider;

        // CORE

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        // PUBLIC

        public void Activate()
        {
            StartCoroutine(StartCooldown());
        }

        public IEnumerator StartCooldown()
        {
            Hide();
            yield return new WaitForSeconds(cooldown);
            Show();
        }

        // PRIVATE

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
