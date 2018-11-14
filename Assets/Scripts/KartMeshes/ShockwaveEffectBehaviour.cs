using System.Collections;
using System.Collections.Generic;
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
        private Inventory _inventory;

        //CORE

        public void Awake()
        {
            _inventory = playerInventory;
        }

        //PUBLIC

        public void InstantiateShockwave(Transform position)
        {
            ShockwavePrefab();
            _shockwavePrefab.transform.position = position.position;
            _shockwavePrefab.transform.rotation = position.rotation;
            StartCoroutine(DisableDelay(_shockwavePrefab));
        }

        /*
        public override void OnEvent(PlayerLaunchItem playerLaunchItem)
        {
            Debug.Log("test");
            InstantiateShockwave(playerLaunchItem.Vector);
        }
        */

        //PRIVATE

        private void ShockwavePrefab()
        {
            _itemNameToDisplay = _inventory.CurrentItem.Name;

            switch (_itemNameToDisplay)
            {
                case "Disk":
                    _shockwavePrefab = shockwaveGreen;
                    break;

                case "Mine":
                    _shockwavePrefab = shockwaveGreen;
                    break;

                case "Guile":
                    _shockwavePrefab = shockwaveYellow;
                    break;

                case "Rocket":
                    _shockwavePrefab = shockwaveBlue;
                    break;

                case "IonBeam":
                    _shockwavePrefab = shockwaveYellow;
                    break;
            }
        }

        IEnumerator DisableDelay(GameObject shockwaveGO)
        {
            shockwaveGO.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            shockwaveGO.SetActive(false);
        }
    }
}

