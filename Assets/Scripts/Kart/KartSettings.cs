using UnityEngine;

namespace Kart
{
    public class KartSettings : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _kartRenderer;
        [SerializeField] private TextMesh _nameText;
        [SerializeField] private GameObject _backCamera;

        // CORE

        private void Awake()
        {
            PhotonView photonView = GetComponentInParent<PhotonView>();

            if (PhotonNetwork.connected && !photonView.isMine)
            {
                SetName(GetPlayer(photonView).NickName);
                Destroy(_backCamera);
            }
        }

        private void Update()
        {
            _nameText.transform.LookAt(Camera.main.transform);
        }

        // PUBLIC

        public void SetColor(Color color)
        {
            _kartRenderer.material.color = color;
        }

        public void SetName(string name)
        {
            _nameText.text = name;
        }

        // PRIVATE

        private PhotonPlayer GetPlayer(PhotonView view)
        {
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if (player.ID == view.ownerId) return player;
            }

            return null;
        }
    }
}
