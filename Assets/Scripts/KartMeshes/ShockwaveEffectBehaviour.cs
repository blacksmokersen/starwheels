using System.Collections;
using UnityEngine;

namespace Items
{
    public class ShockwaveEffectBehaviour : Bolt.EntityEventListener
    {
        [SerializeField] private GameObject shockwaveGreen;
        [SerializeField] private GameObject shockwaveBlue;
        [SerializeField] private GameObject shockwaveYellow;
        [SerializeField] private Inventory playerInventory;

        private GameObject _shockwavePrefab;
        private string _itemNameToDisplay;

        //BOLT

        public override void OnEvent(PlayerLaunchItem playerLaunchItem)
        {
            if (entity == playerLaunchItem.Entity)
                InstantiateShockwave(playerLaunchItem.Position, playerLaunchItem.Rotation, playerLaunchItem.ItemName);
        }

        //PUBLIC

        public void InstantiateShockwave(Vector3 position, Quaternion rotation, string itemName)
        {
            ShockwavePrefab(itemName);
            _shockwavePrefab.transform.position = position;
            _shockwavePrefab.transform.rotation = rotation;
            StartCoroutine(DisableDelay(_shockwavePrefab));
        }

        //PRIVATE

        private void ShockwavePrefab(string itemName)
        {
            _itemNameToDisplay = itemName;

            switch (_itemNameToDisplay)
            {
                case "Disk(Clone)":
                    _shockwavePrefab = shockwaveGreen;
                    break;

                case "Mine(Clone)":
                    _shockwavePrefab = shockwaveGreen;
                    break;

                case "Guile(Clone)":
                    _shockwavePrefab = shockwaveYellow;
                    break;

                case "Rocket(Clone)":
                    _shockwavePrefab = shockwaveBlue;
                    break;

                case "IonBeam(Clone)":
                    _shockwavePrefab = shockwaveYellow;
                    break;

                case "TPBack(Clone)":
                    _shockwavePrefab = shockwaveYellow;
                    break;
                case "Totem(Clone)":
                    _shockwavePrefab = shockwaveYellow;
                    break;
            }
        }

        private IEnumerator DisableDelay(GameObject shockwaveGO)
        {
            shockwaveGO.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            shockwaveGO.SetActive(false);
        }
    }
}

