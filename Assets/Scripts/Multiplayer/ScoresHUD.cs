using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer
{
    public class ScoresHUD : MonoBehaviour
    {

        [Header("Players")]
        public Text PlayerList;

        private void Awake()
        {
            UpdatePlayerList();
        }


        public void UpdatePlayerList()
        {
            PlayerList.text = null;
            /*
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                PlayerList.text += player.NickName + "\t Score: " + player.GetScore() + "\n";
            }
            */
        }
    }
}
