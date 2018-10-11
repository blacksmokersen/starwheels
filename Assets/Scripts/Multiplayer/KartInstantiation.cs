using UnityEngine;

namespace Multiplayer
{
    [BoltGlobalBehaviour]
    public class KartInstant : Bolt.GlobalEventListener
    {
        public override void SceneLoadLocalDone(string map)
        {
            int randomX = Random.Range(-4, 4);
            BoltNetwork.Instantiate(BoltPrefabs.KartRefacto, new Vector3(randomX, 1, 0), Quaternion.identity);
        }
    }
}
