using UnityEngine;
using UnityEngine.UI;

namespace Photon.Lobby
{
    /*
     * Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
     *
     */
    public class LobbyMainMenu : MonoBehaviour
    {
        [Header("Main menu elements")]
        [SerializeField] private LobbyManager lobbyManager;
        [SerializeField] private RectTransform lobbyServerList;
        [SerializeField] private RectTransform lobbyPanel;
        [SerializeField] private InputField matchNameInput;

        // CORE

        public void OnEnable()
        {
            lobbyManager.TopPanel.ToggleVisibility(true);

            matchNameInput.onEndEdit.RemoveAllListeners();
            matchNameInput.onEndEdit.AddListener(OnEndEditGameName);
        }

        // PRIVATE

        private void OnClickCreateMatchmakingGame()
        {
            lobbyManager.CreateMatch(matchNameInput.text);
            lobbyManager.BackDelegate = LobbyManager.Instance.Shutdown;
            lobbyManager.DisplayIsConnecting();
            lobbyManager.SetServerInfo("Matchmaker Host", LobbyManager.Instance.MatchHost);
        }

        private void OnClickOpenServerList()
        {
            lobbyManager.StartClient();
            lobbyManager.BackDelegate = lobbyManager.SimpleBackCallback;
            lobbyManager.ChangeTo(lobbyServerList);
        }

        private void OnClickJoinRandom()
        {
            // TODO
        }

        private void OnEndEditGameName(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickCreateMatchmakingGame();
            }
        }
    }
}
