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

        public void InstantiateShockwave(Vector3 position,Quaternion rotation,string itemName)
        {
            ShockwavePrefab(itemName);
            _shockwavePrefab.transform.position = position;
            _shockwavePrefab.transform.position = position;
            StartCoroutine(DisableDelay(_shockwavePrefab));
            //   _shockwavePrefab.transform.rotation = position.rotation;
            // StartCoroutine(DisableDelay(_shockwavePrefab));
            /*
            var launchEvent = PlayerLaunchItem.Create(entity);
            launchEvent.GameObjectName = _shockwavePrefab.name;
            launchEvent.Position = position.position;
            launchEvent.Entity = GetComponentInParent<BoltEntity>();
            launchEvent.Send();
            */
        }

        public override void OnEvent(PlayerLaunchItem playerLaunchItem)
        {
            /*
            Debug.Log(GameObject.Find(playerLaunchItem.GameObjectName));
            GameObject TargetGameobject = GameObject.Find(playerLaunchItem.GameObjectName);
            Debug.Log(TargetGameobject);
            */
            if (GetComponentInParent<BoltEntity>() == playerLaunchItem.Entity)
            {
                InstantiateShockwave(playerLaunchItem.Position,playerLaunchItem.Rotation,playerLaunchItem.ItemName);
            }
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

