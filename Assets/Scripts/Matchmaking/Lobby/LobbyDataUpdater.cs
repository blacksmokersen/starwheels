using UnityEngine;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class LobbyDataUpdater : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private LobbyData _lobbyData;

        public void UpdateGameName(string name)
        {
            _lobbyData.ServerName = name;
        }
    }
}
