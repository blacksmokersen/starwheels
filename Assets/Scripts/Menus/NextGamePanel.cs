using UnityEngine;
using SW.Matchmaking;

namespace Menu
{
    public class NextGamePanel : MonoBehaviour
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        private string _nextGamemode;
        private string _nextMap;

        // PUBLIC

        public void StartNextGame()
        {
            if (BoltNetwork.IsServer)
            {
                SWMatchmaking.SetLobbyData(_lobbyData);
                BoltNetwork.LoadScene(_nextMap);
            }
        }

        // PRIVATE

        private void SetNextGamemode(string nextGamemode)
        {
            _lobbyData.ChosenGamemode = nextGamemode;
        }

        private void SetNextMap(string nextMap)
        {
            _lobbyData.ChosenMapName = nextMap;
        }

        private void SetNextSkin(string nextSkin)
        {

        }
    }
}
