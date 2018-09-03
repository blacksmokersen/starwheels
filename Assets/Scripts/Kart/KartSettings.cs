using UnityEngine;

namespace Kart
{
    public class KartSettings : BaseKartComponent
    {
        [SerializeField] private MeshRenderer kartRenderer;
        [SerializeField] private TextMesh nameText;
        [SerializeField] private GameObject backCamera;

        // CORE

        private new void Awake()
        {
            base.Awake();
            if (PhotonNetwork.connected && !photonView.isMine)
            {
                SetName(GetPlayer(photonView).NickName);
                Destroy(backCamera);
            }
            SetColor(PhotonNetwork.player.GetTeam());
        }

        private void Update()
        {
            nameText.transform.LookAt(Camera.main.transform);
        }

        // PUBLIC

        public void SetColor(PunTeams.Team team)
        {
            switch (team)
            {
                case PunTeams.Team.blue:
                    kartRenderer.material = Resources.Load<Material>(Constants.BlueKartTextureName);
                    break;
                case PunTeams.Team.red:
                    kartRenderer.material = Resources.Load<Material>(Constants.RedKartTextureName);
                    break;
                default:
                    break;
            }
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
