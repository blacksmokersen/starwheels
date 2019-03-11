using UnityEngine;
using SW.Matchmaking;
using Bolt;

namespace Menu
{
    public class NextGamePanel : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        // BOLT

        public override void OnEvent(GameOver evnt)
        {
            gameObject.SetActive(true);
        }

        // PUBLIC

        public void StartNextGame()
        {
            if (BoltNetwork.IsServer)
            {
                SWMatchmaking.SetLobbyData(_lobbyData);
                var token = FindObjectOfType<Multiplayer.SpawnAssigner>().RoomInfoToken;
                if (token == null)
                {
                    token = new Photon.RoomProtocolToken() { } ;
                }
                BoltNetwork.LoadScene(_lobbyData.ChosenMapName, token);
            }
        }

        public void SetNextGamemode(string nextGamemode)
        {
            _lobbyData.ChosenGamemode = nextGamemode;
        }

        public void SetNextMap(string nextMap)
        {
            _lobbyData.ChosenMapName = nextMap;
        }

        public void SetNextSkin(string nextSkin)
        {

        }
    }
}
