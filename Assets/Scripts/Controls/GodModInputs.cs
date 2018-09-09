using UnityEngine;
using Items;

namespace Controls
{
    public class GodModInputs : BaseKartComponent
    {
        private bool _enabled = true;
        private int _currentItemIndex = 0;

        // CORE

        private void Update()
        {
            if (!_enabled) return;

            if (photonView.IsMine)
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

        // PUBLIC

        // PRIVATE

        private void SetUnlimitedItems()
        {
            kartHub.kartInventory.SetCount(1000);
        }

        private void SwitchToNextItem()
        {
            var items = ItemsLottery.Items;

            kartHub.kartInventory.SetItem(items[(_currentItemIndex++) % items.Length]);
            SetUnlimitedItems();
        }

        private void LoseOneLife()
        {
            kartHub.kartHealthSystem.HealthLoss();
        }

        private void ResetLives()
        {
            kartHub.kartHealthSystem.ResetLives();
        }
    }
}
