using UnityEngine;

namespace Multiplayer
{
    public class KartInstantiation : Bolt.GlobalEventListener
    {
        private void Awake()
        {
            if (!BoltNetwork.isConnected)
            {
                BoltLauncher.StartServer();
                Debug.Log("Starting bolt !");
            }
        }

        public override void BoltStartDone()
        {
            var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart, new Vector3(0, 2, 0), Quaternion.identity);
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);
        }
    }
}
