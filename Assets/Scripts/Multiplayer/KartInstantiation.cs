using UnityEngine;

namespace Multiplayer
{
    public class KartInstantiation : Bolt.GlobalEventListener
    {
        private bool _kartInstantiated = false;

        private void Awake()
        {
            if (!BoltNetwork.isConnected)
            {
                BoltLauncher.StartServer();
                Debug.Log("Starting bolt !");
            }
        }

        private void Start()
        {
            InstantiateKart();
        }

        public override void BoltStartDone()
        {
            InstantiateKart();
        }

        private void InstantiateKart()
        {
            if (!_kartInstantiated)
            {
                var myKart = BoltNetwork.Instantiate(BoltPrefabs.Kart, new Vector3(0, 2, 0), Quaternion.identity);
                FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);
                _kartInstantiated = true;
            }
        }
    }
}
