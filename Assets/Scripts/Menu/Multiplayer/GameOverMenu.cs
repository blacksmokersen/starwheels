using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Menu
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button replayButton;

        private void Awake()
        {
            replayButton.enabled = PhotonNetwork.IsMasterClient;
        }
    }
}
