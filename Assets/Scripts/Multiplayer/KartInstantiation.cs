using UnityEngine;

namespace Multiplayer
{
    public class KartInstantiation : Bolt.GlobalEventListener
    {
        private void Awake()
        {
            Debug.Log("Bolt is connected : " + BoltNetwork.isConnected);
            if (!BoltNetwork.isConnected)
            {
                BoltLauncher.StartSinglePlayer();
            }
        }

        public override void BoltStartDone()
        {
            Debug.Log("INSTANTIATION");
            var myKart = BoltNetwork.Instantiate(BoltPrefabs.KartBob, new Vector3(0, 1, 0), Quaternion.identity);
            //FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);
        }
    }
}
