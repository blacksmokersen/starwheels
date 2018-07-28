using UnityEngine;
using Kart;
using Items;
using HUD;

namespace Controls
{
    public class GodModInputs : MonoBehaviour
    {
        public bool Enabled = true;

        private KartActions kartAction;
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

        public void SetKart(KartActions value)
        {
            kartAction = value;
        }

        public void SetUnlimitedItems()
        {
            Debug.Log("Set Unlimited Items");
            kartAction.kartInventory.Count = 1000;
        }

        public void SwitchToNextItem()
        {
            Debug.Log("Switched to Next Item");
            var kartInventory = kartAction.kartInventory;
            var items = ItemsLottery.Items;
            var itemIndex = (ActualItemIndex++) % items.Length;
            kartInventory.Item = items[itemIndex];
            FindObjectOfType<HUDUpdater>().SetItem(kartInventory.Item);
            SetUnlimitedItems();
        }

        public void SpawnEnemyKart()
        {
            Debug.Log("Spawned Enemy Kart");
            var game = FindObjectOfType<Game>();
            game.SpawnKart(kartAction.gameObject.transform.position + Vector3.forward);
        }

        public void LoseOneLife()
        {
            Debug.Log("Lost One Life");
            kartAction.kartHealthSystem.HealthLoss();
        }

        public void ResetLives()
        {
            Debug.Log("Reset Lives");
            kartAction.kartHealthSystem.ResetLives();
        }
    }
}