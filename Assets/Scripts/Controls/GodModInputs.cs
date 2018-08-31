using UnityEngine;
using Items;

namespace Controls
{
    public class GodModInputs : BaseKartComponent
    {
        public bool Enabled = true;

        private int ActualItemIndex = 0;

        public void Update()
        {
            if (!enabled) return;

            if (photonView.isMine)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    SetUnlimitedItems();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    SwitchToNextItem();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    LoseOneLife();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    ResetLives();
                }
            }
        }

        public void SetUnlimitedItems()
        {
            kartHub.kartInventory.Count = 1000;
        }

        public void SwitchToNextItem()
        {
            var kartInventory = kartHub.kartInventory;
            var items = ItemsLottery.Items;
            var itemIndex = (ActualItemIndex++) % items.Length;
            kartInventory.Item = items[itemIndex];
            kartEvents.OnItemUsed(kartInventory.Item, kartInventory.Count);
            SetUnlimitedItems();
        }

        public void LoseOneLife()
        {
            kartHub.kartHealthSystem.HealthLoss();
        }

        public void ResetLives()
        {
            kartHub.kartHealthSystem.ResetLives();
        }
    }
}
