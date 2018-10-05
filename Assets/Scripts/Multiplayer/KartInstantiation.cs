using UnityEngine;

namespace Multiplayer
{
    [BoltGlobalBehaviour]
    public class KartInstant : Bolt.GlobalEventListener
    {

        public override void SceneLoadLocalDone(string map)
        {
            BoltNetwork.Instantiate(BoltPrefabs.KartRefacto_1, new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}
