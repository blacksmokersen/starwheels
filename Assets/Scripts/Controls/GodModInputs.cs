using UnityEngine;
using Kart;
using Items;
using HUD;

namespace Controls
{
    public class GodModInputs : BaseKartComponent
    {
        public bool Enabled = true;

        private KartHub kartAction;
        private int ActualItemIndex = 0;

        public void Update()
        {
            if (!enabled) return;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetUnlimitedItems();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchToNextItem();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SpawnEnemyKart();
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

        public void SetKart(KartHub value)
        {
            kartAction = value;
        }

        public void SetUnlimitedItems()
        {
            kartAction.kartInventory.Count = 1000;
        }

        public void SwitchToNextItem()
        {
            var kartInventory = kartAction.kartInventory;
            var items = ItemsLottery.Items;
            var itemIndex = (ActualItemIndex++) % items.Length;
            kartInventory.Item = items[itemIndex];
            FindObjectOfType<HUDUpdater>().UpdateItem(kartInventory.Item,kartInventory.Count);
            SetUnlimitedItems();
        }

        public void SpawnEnemyKart()
        {
            var game = FindObjectOfType<Game>();
            game.SpawnKart(kartAction.gameObject.transform.position + Vector3.forward);
        }

        public void LoseOneLife()
        {
            kartAction.kartHealthSystem.HealthLoss();
        }

        public void ResetLives()
        {
            kartAction.kartHealthSystem.ResetLives();
        }
    }
}