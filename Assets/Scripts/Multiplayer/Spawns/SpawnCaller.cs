using System.Collections;
using UnityEngine;

namespace Multiplayer
{
    public class SpawnCaller : MonoBehaviour
    {
        [SerializeField] private SpawnSettings _settings;

        // PUBLIC

        public void CallForSpawn(PlayerSettings player)
        {
            if (_settings.RespawnOn)
            {
                StartCoroutine(CallForSpawnAfterXSeconds(_settings.SecondsBeforeRespawn, player));
            }
        }

        // PRIVATE

        private IEnumerator CallForSpawnAfterXSeconds(float x, PlayerSettings player)
        {
            yield return new WaitForSeconds(x);
            SendKartDestroyedEvent(player);
        }

        private void SendKartDestroyedEvent(PlayerSettings player)
        {
            KartDestroyed kartDestroyedEvent = KartDestroyed.Create();
            kartDestroyedEvent.Team = player.TeamColor;
            kartDestroyedEvent.ConnectionID = player.ConnectionID;
            kartDestroyedEvent.Send();
        }
    }
}
