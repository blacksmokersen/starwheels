using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class WallPrefabScript : MonoBehaviour
    {
        [SerializeField] private WallSettings _wallSettings;

        //CORE

        private void Start()
        {
            StartCoroutine(SelfDestruct(_wallSettings.WallDuration));
        }

        //PRIVATE

        IEnumerator SelfDestruct(float wallDuration)
        {
            yield return new WaitForSeconds(wallDuration);
            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
            destroyEntityEvent.Entity = GetComponent<BoltEntity>();
            destroyEntityEvent.Send();

        }
    }

}
