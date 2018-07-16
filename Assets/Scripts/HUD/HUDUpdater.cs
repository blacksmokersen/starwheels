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
        }

        IEnumerator UpdateRoutine()
        {
            while (Application.isPlaying)
            {
                TimeText.text = "Time : " + Time.time;
                SpeedText.text = "Speed : " + kartRigidBody.velocity.magnitude;
                FPSText.text = "FPS : " + 1.0f / Time.deltaTime;
                compteur.CompteurBehaviour(kartRigidBody.velocity.magnitude);
                UpdateItems();
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void UpdateItems()
        {
            var inventory = FindObjectOfType<KartInventory>();

            switch (inventory.InventoryItem)
            {
                case ItemTypes.Disk:
                    InventoryItemTexture.texture = DiskImage;
                    break;
                case ItemTypes.Nitro:
                    InventoryItemTexture.texture = NitroImage;
                    break;
                case ItemTypes.Rocket:
                    InventoryItemTexture.texture = RocketImage;
                    break;
                case ItemTypes.Mine:
                    InventoryItemTexture.texture = MineImage;
                    break;
                case ItemTypes.None:
                    InventoryItemTexture.texture = null;
                    break;
            }

            switch (inventory.StackedItem)
            {
                case ItemTypes.Disk:
                    StackedItemTexture.texture = DiskImage;
                    break;
                case ItemTypes.Nitro:
                    StackedItemTexture.texture = NitroImage;
                    break;
                case ItemTypes.Rocket:
                    StackedItemTexture.texture = RocketImage;
                    break;
                case ItemTypes.Mine:
                    StackedItemTexture.texture = MineImage;
                    break;
                case ItemTypes.None:
                    StackedItemTexture.texture = null;
                    break;
            }
        }
    }
}