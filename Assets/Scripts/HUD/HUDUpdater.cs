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

        private Rigidbody kartRigidBody;
        
        public RawImage InventoryItemTexture;
        public RawImage ItemTexture;

        public void SetKart(Rigidbody body)
        {
            kartRigidBody = body;
            StartCoroutine(UpdateRoutine());

            SetItem(null);
            
            KartEvents.OnAccelerate += (vel) => SpeedText.text = "Speed : " + vel;
        }
        
        IEnumerator UpdateRoutine()
        {
            while (Application.isPlaying)
            {
                TimeText.text = "Time : " + Time.time;
                FPSText.text = "FPS : " + 1.0f / Time.deltaTime;
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void SetItem(ItemData item)
        {
            ItemTexture.texture 
                = item != null ? item.InventoryTexture : null;
        }
    }
}