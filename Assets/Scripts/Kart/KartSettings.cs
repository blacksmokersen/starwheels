using UnityEngine;

namespace Kart
{
    public class KartSettings : MonoBehaviour
    {
        [SerializeField] private MeshRenderer kartRenderer;
        [SerializeField] private TextMesh nameText;
        [SerializeField] private GameObject backCamera;

        // CORE

        private void Awake()
        {
            PhotonView view = GetComponentInParent<PhotonView>();

            if (PhotonNetwork.connected && !view.isMine)
            {
                SetName(GetPlayer(view).NickName);
                Destroy(backCamera);
            }
        }

        private void Update()
        {
            nameText.transform.LookAt(Camera.main.transform);
        }

        // PUBLIC

        public void SetColor(Color color)
        {
            kartRenderer.material.color = color;
        }

        public void SetName(string name)
        {
            nameText.text = name;
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
