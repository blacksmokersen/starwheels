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
            SendRespawnRequestEvent(player);
        }

        private void SendRespawnRequestEvent(PlayerSettings player)
        {
            RespawnRequest respawnRequestEvent = RespawnRequest.Create();
            respawnRequestEvent.Team = player.TeamColor;
            respawnRequestEvent.ConnectionID = player.ConnectionID;
            respawnRequestEvent.Send();
        }
    }
}
