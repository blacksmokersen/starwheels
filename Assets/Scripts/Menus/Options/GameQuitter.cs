using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bolt;

namespace Menu
{
    [DisallowMultipleComponent]
    public class GameQuitter : GlobalEventListener
    {
        // BOLT

        public override void BoltShutdownBegin(AddCallback registerDoneCallback)
        {
            SceneManager.LoadScene("Menu");

            registerDoneCallback(() =>
            {
                BoltLog.Warn("Bolt is down");
            });
        }

        public override void OnEvent(AllPlayersToMenu DisconnectAllPlayers)
        {
            if (BoltNetwork.IsClient)
            {
                QuitMatch();
            }
        }

        // PUBLIC

        public void QuitMatch()
        {
            if (BoltNetwork.IsServer)
            {
                SendAllToMenuEvent();
                StartCoroutine(HostDisconectLastSecurity());
            }
            else if (BoltNetwork.IsClient)
            {
                BoltLauncher.Shutdown();
            }
        }

        // PRIVATE

        private void SendAllToMenuEvent()
        {
            AllPlayersToMenu.Create().Send();
        }

        private IEnumerator HostDisconectLastSecurity()
        {
            yield return new WaitForSeconds(1.5f);
            BoltLauncher.Shutdown();
        }
    }
}
