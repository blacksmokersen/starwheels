using UnityEngine;

namespace Multiplayer
{
    [BoltGlobalBehaviour]
    public class KartInstantiation : Bolt.GlobalEventListener
    {
        public override void SceneLoadLocalDone(string map)
        {
            var myKart = BoltNetwork.Instantiate(BoltPrefabs.KartBob, new Vector3(0, 1, 0), Quaternion.identity);
            FindObjectOfType<CameraUtils.SetKartCamera>().SetKart(myKart);
        }
    }
}
