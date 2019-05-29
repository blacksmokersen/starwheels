using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Network.Ping
{
    [DisallowMultipleComponent]
    public class RegionPingChecker : MonoBehaviour
    {
        public bool Enabled;

        [Header("Regions")]
        public bool Asia;

        [Header("Settings")]
        public float SecondsBetweenRefreshes;

        // CORE

        private void Update()
        {
            if (Enabled)
            {

            }
        }
    }
}
