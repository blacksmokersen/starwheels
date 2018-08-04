using UnityEngine;
using Photon;

namespace Kart
{
    public class KartSettings : PunBehaviour
    {
        [SerializeField] private MeshRenderer kartRenderer;
        [SerializeField] private TextMesh nameText;
        [SerializeField] private GameObject backCamera;

        private void Awake()
        {
            if (PhotonNetwork.connected && !photonView.isMine)
            {
                SetName(GetPlayer(photonView).NickName);
                Destroy(backCamera);
            }
        }

        public void SetColor(Color color)
        {
            kartRenderer.material.color = color;
        }

        public void SetName(string name)
        {
            nameText.text = name;
        }

        private void Update()
        {
            nameText.transform.LookAt(Camera.main.transform);
        }

        private PhotonPlayer GetPlayer(PhotonView view)
        {
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
                if (player.ID == view.ownerId)
                    return player;
            return null;
        }
    }
}
