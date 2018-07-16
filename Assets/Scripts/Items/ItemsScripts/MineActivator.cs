using System.Collections;
using UnityEngine;

namespace Items{
    public class MineActivator : MonoBehaviour
    {
        public float ActivationTime;

        private void Start()
        {
            StartCoroutine(MineActivationDelay());
        }

        IEnumerator MineActivationDelay()
        {
            yield return new WaitForSeconds(ActivationTime);
            GetComponentInChildren<PlayerMineTrigger>().Activated = true;
            GetComponentInChildren<ItemMineTrigger>().Activated = true;
        }
    }
}