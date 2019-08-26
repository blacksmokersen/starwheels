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
            if (BoltNetwork.IsServer)
            {
                _panel.SetActive(true);
            }
        }

        public override void OnEvent(UpdateNextGameMode evnt)
        {
            if (BoltNetwork.IsClient)
            {
                _lobbyData.SetGamemode(evnt.NextGameMode);
                Debug.LogError("UPDATE GAMEMODE TO : " + evnt.NextGameMode);
            }
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
                    PlayersCount = SWMatchmaking.GetCurrentLobbyPlayerCount(),
                    RoomInfo = "test"
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

            UpdateNextGameMode updateNextGameMode = UpdateNextGameMode.Create();
            updateNextGameMode.NextGameMode = nextGamemode;
            updateNextGameMode.Send();
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
