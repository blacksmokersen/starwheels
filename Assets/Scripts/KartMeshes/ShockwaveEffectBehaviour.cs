using UnityEngine;
using Bolt;

namespace Items
{
    public class ShockwaveEffectBehaviour : EntityEventListener
    {
        private ItemListData _itemList;

        // CORE

        private void Awake()
        {
            _itemList = Resources.Load<ItemListData>(Constants.Resources.ItemListData);
        }

        //BOLT

        public override void OnEvent(PlayerLaunchItem playerLaunchItem)
        {
            if (entity == playerLaunchItem.Entity)
            {
                Debug.Log("This entity : " + playerLaunchItem.ItemName);
                InstantiateShockwave(playerLaunchItem.Position, playerLaunchItem.Rotation, playerLaunchItem.ItemName);
            }
        }

        //PUBLIC

        public void InstantiateShockwave(Vector3 position, Quaternion rotation, string itemName)
        {
            var item = _itemList.GetItemUsingName(itemName);
            if (item)
            {
                var shockwave = Instantiate(item.ShockwavePrefab);
                shockwave.transform.position = position;
                shockwave.transform.rotation = rotation;
                Destroy(shockwave, .3f);
            }
        }
    }
}

