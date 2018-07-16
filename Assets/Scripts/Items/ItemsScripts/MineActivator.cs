using System.Collections;
using UnityEngine;

namespace Items{
    public class MineActivator : ItemBehaviour
    {
        public float ActivationTime;

        private void Start()
        {
            StartCoroutine(MineActivationDelay());
        }

        public override void SetOwner(KartInventory kart)
        {
            transform.position = kart.ItemPositions.BackPosition.position;
        }

        IEnumerator MineActivationDelay()
        {
            yield return new WaitForSeconds(ActivationTime);
            GetComponentInChildren<PlayerMineTrigger>().Activated = true;
            GetComponentInChildren<ItemMineTrigger>().Activated = true;
        }
    }
}