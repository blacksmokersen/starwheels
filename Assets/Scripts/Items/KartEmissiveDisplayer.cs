using UnityEngine;
using Bolt;

namespace Items
{
    [RequireComponent(typeof(Inventory))]
    public class KartEmissiveDisplayer : GlobalEventListener
    {
        [Header("Materials Emissives")]
        public Material DefaultMaterial;

        [Space(10)]
        public GameObject Emissive;

        private ItemListData _itemList;

        // CORE

        private void Awake()
        {
            _itemList = Resources.Load<ItemListData>(Constants.Resources.ItemListData);
        }

        // BOLT

        public override void OnEvent(ShowKartDisplayItem evnt)
        {
            var entity = GetComponentInParent<BoltEntity>();

            if (entity == evnt.Entity)
            {
                if (evnt.ItemCount > 0)
                {
                    DisplayEmissive(evnt.ItemName);
                }
                else
                {
                    HideEmissive();
                }
            }
        }

        // PUBLIC

        public void DisplayEmissive(string itemName)
        {
            var item = _itemList.GetItemUsingName(itemName);
            if (item)
            {
                Emissive.GetComponent<MeshRenderer>().material = item.EmissiveMaterial;
            }
        }

        public void HideEmissive()
        {
            Emissive.GetComponent<MeshRenderer>().material = DefaultMaterial;
        }
    }
}
