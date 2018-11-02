using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class RowRoom : MonoBehaviour
    {
        [SerializeField] private Text roomNameText;
        [SerializeField] private Text playerCountText;

        // CORE

        private void Awake()
        {
            //GetComponent<Button>().onClick.AddListener(() => PhotonNetwork.JoinRoom(roomNameText.text));
        }

        // PUBLIC

        public void UpdateRoomName(string name)
        {
            roomNameText.text = name;
        }

        public void UpdatePlayerCount(int playerCount, int maxPlayerCount)
        {
            playerCountText.text = "" + playerCount + " / " + maxPlayerCount;
        }

        // PRIVATE
    }
}
