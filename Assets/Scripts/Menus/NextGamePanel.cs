using UnityEngine;
using UnityEngine.SceneManagement;
using SW.Matchmaking;
using Bolt;

namespace Menu
{
    public class NextGamePanel : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("UI Elements")]
        [SerializeField] private GameObject _panel;

        // BOLT

        public override void OnEvent(GameOver evnt)
        {
            _panel.SetActive(true);
        }

        // PUBLIC

        public void StartNextGame()
        {
            if (BoltNetwork.IsServer)
            {
                SWMatchmaking.SetLobbyData(_lobbyData);

                var token = new Multiplayer.RoomProtocolToken()
                {
                    Gamemode = _lobbyData.ChosenGamemode,
                    PlayersCount = SWMatchmaking.GetCurrentLobbyPlayerCount()
                };
                BoltNetwork.LoadScene(_lobbyData.ChosenMapName, token);
            }
        }

        public void BackToMenu()
        {
            BoltLauncher.Shutdown();
            SceneManager.LoadScene("Menu");
        }

        public void SetNextGamemode(string nextGamemode)
        {
            _lobbyData.SetGamemode(nextGamemode);
        }

        public void SetNextMap(string nextMap)
        {
            _lobbyData.SetMap(nextMap);
        }

        public void SetNextSkin(string nextSkin)
        {

        }
    }
}
