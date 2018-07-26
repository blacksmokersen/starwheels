using UnityEngine;
using Kart;
using Items;

namespace Controls
{
    public class GodModInputs : MonoBehaviour
    {
        public bool Enabled = true;
        private KartActions kartAction;

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
            kartAction.gameObject.GetComponent<KartInventory>().Count = 1000;
        }

        public void SwitchToNextItem()
        {
            Debug.Log("Switched to Next Item");
            var kartInventory = kartAction.gameObject.GetComponent<KartInventory>();
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
        }

        public void ResetLives()
        {
            Debug.Log("Reset Lives");
        }
    }
}