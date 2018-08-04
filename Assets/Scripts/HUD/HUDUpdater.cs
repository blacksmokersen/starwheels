using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Items;

namespace HUD
{
    public class HUDUpdater : BaseKartComponent
    {
        /*
        public Text SpeedText;
        public Text TimeText;
        public Text FPSText;
        */
        public Text ItemCountText;
        
        public RawImage ItemTexture;

        private new void Awake()
        {
            base.Awake();
            kartEvents.OnItemUsed += UpdateItem;
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
            ItemTexture.texture = item != null ? item.InventoryTexture : null;
            ItemCountText.text = "" + count;
        }
    }
}