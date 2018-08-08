using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Items;
using Kart;

namespace HUD
{
    public class HUDUpdater : MonoBehaviour
    {
        /*
        public Text SpeedText;
        public Text TimeText;
        public Text FPSText;
        */
        public Text ItemCountText;        
        public Image ItemTexture;
        public Image ItemFrame;

        private void Start()
        {
            KartEvents.Instance.OnItemUsed += UpdateItem;
            UpdateItem(null, 0);
        }

        public void SetKart(Rigidbody body)
        {
            StartCoroutine(UpdateRoutine());
        }
        
        IEnumerator UpdateRoutine()
        {
            while (Application.isPlaying)
            {
                //TimeText.text = "Time : " + Time.time;
                //FPSText.text = "FPS : " + 1.0f / Time.deltaTime;
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void UpdateItem(ItemData item, int count)
        {
            ItemTexture.sprite = item != null ? item.InventoryTexture : null;
            ItemFrame.color = item != null ? item.ItemColor : Color.white ;
            ItemCountText.text = "" + count;
        }
    }
}