using UnityEngine;
using UnityEngine.UI;
using Bolt;
using TMPro;
using System.Collections;

namespace SW.Matchmaking
{
    public class TinyLobbyUpdater : GlobalEventListener
    {
        [Header("Lobby Info")]
        [SerializeField] private LobbyData _lobbyData;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _lookingForGameText;
        [SerializeField] private TextMeshProUGUI _currentPlayerCountText;
        [SerializeField] private Button _startGameButton;

        // BOLT

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            gameObject.SetActive(false);
        }

        public override void Connected(BoltConnection connection)
        {
            if (connection.ConnectionId == SWMatchmaking.GetMyBoltId())
            {
                Debug.LogError("I Connected");
                SetLookingForPlayers();
            }
            Debug.Log("A player joined the lobby.");
            UpdateCurrentPlayerCount();
        }

        public override void Disconnected(BoltConnection connection)
        {
            Debug.Log("A player disconnected from the lobby.");
            UpdateCurrentPlayerCount();
        }

        // PUBLIC

        public void SetLookingForGame()
        {
            gameObject.SetActive(true);
            _lookingForGameText.gameObject.SetActive(true);
            _currentPlayerCountText.gameObject.SetActive(false);
            _startGameButton.gameObject.SetActive(false);
        }

        public void SetLookingForPlayers()
        {
            gameObject.SetActive(true);
            _lookingForGameText.gameObject.SetActive(false);
            _currentPlayerCountText.gameObject.SetActive(true);
            _startGameButton.gameObject.SetActive(true);

            StartCoroutine(UpdatePlayerCountRoutine());
        }

        public void LaunchGame()
        {
            if (BoltNetwork.IsServer)
            {
                var roomToken = new Photon.RoomProtocolToken() { Gamemode = _lobbyData.ChosenGamemode };
                BoltNetwork.LoadScene(_lobbyData.ChosenMapName, roomToken);
            }
            else
            {
                Debug.LogWarning("Can't launch game if you are not the server.");
            }
        }

        public void QuitLobby()
        {
            BoltLauncher.Shutdown();
        }

        // PRIVATE

        private IEnumerator UpdatePlayerCountRoutine()
        {
            Debug.Log("Starting routine");

            while (_currentPlayerCountText.gameObject.activeInHierarchy)
            {
                Debug.Log("Updating player count");
                UpdateCurrentPlayerCount();
                yield return new WaitForSeconds(1f);
            }
        }

        private void UpdateCurrentPlayerCount()
        {
            var playerCount = 1 + SWMatchmaking.GetCurrentLobbyPlayerCount();
            _currentPlayerCountText.text = playerCount + " players";
        }

        private void OnApplicationQuit()
        {
            QuitLobby();
        }
    }
}
