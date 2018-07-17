using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Items;

namespace HUD
{
    public class HUDUpdater : MonoBehaviour
    {
        public Text SpeedText;
        public Text TimeText;
        public Text FPSText;

        public GameObject Compteur;
        private Compteur compteur;

        private Rigidbody kartRigidBody;

        public Texture MineImage;
        public Texture RocketImage;
        public Texture DiskImage;
        public Texture NitroImage;

        public RawImage InventoryItemTexture;
        public RawImage StackedItemTexture;

        public void SetKart(Rigidbody body)
        {
            compteur = Compteur.GetComponent<Compteur>();
            kartRigidBody = body;
            StartCoroutine(UpdateRoutine());

            SetItem(null, null);
        }

        IEnumerator UpdateRoutine()
        {
            while (Application.isPlaying)
            {
                TimeText.text = "Time : " + Time.time;
                SpeedText.text = "Speed : " + kartRigidBody.velocity.magnitude;
                FPSText.text = "FPS : " + 1.0f / Time.deltaTime;
                compteur.CompteurBehaviour(kartRigidBody.velocity.magnitude);
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void SetItem(ItemData stacked, ItemData item)
        {
            InventoryItemTexture.texture = item != null ? item.InventoryTexture : null;
            StackedItemTexture.texture = stacked != null ? stacked.InventoryTexture : null;
        }
    }
}