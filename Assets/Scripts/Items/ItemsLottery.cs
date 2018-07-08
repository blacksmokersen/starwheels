using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Items
{
    public class ItemsLottery : MonoBehaviour
    {
        List<GameObject> Items;
        
        private GameObject PickRandomItem()
        {
            return Items[Random.Range(0, Items.Count)];
        }
    }
}
