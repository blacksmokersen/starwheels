using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

namespace Kart
{
    public class KartMultiplayerSettings : BaseKartComponent
    {
        [SerializeField] private MeshRenderer kartRenderer;
        [SerializeField] private TextMesh nameText;
        [SerializeField] private GameObject backCamera;

        // CORE

        private new void Awake()
        {
            base.Awake();
            photonView = GetComponent<PhotonView>();
            if (PhotonNetwork.IsConnected && !photonView.IsMine)
            {
                SetName(GetPlayer(photonView).NickName);
                Destroy(backCamera);
            }
            SetColor(GetPlayer(photonView).GetTeam());
        }

        private void Update()
        {
            nameText.transform.LookAt(Camera.main.transform);
        }

        // PUBLIC

        public void SetColor(PunTeams.Team team)
        {
            string teamStr = "default";
            switch (team)
            {
                case PunTeams.Team.blue:
                    teamStr = "blue";
                    break;
                case PunTeams.Team.red:
                    teamStr = "red";
                    break;
                default:
                    break;
            }
            photonView.RPC("RPCSetColor", RpcTarget.AllBuffered, teamStr);
        }

        [PunRPC]
        public void RPCSetColor(string team)
        {
            switch (team)
            {
                case "blue":
                    kartRenderer.material = Resources.Load<Material>(Constants.BlueKartTextureName);
                    break;
                case "red":
                    kartRenderer.material = Resources.Load<Material>(Constants.RedKartTextureName);
                    break;
                default:
                    break;
            }
        }

        public void SetName(string name)
        {
            photonView.RPC("RPCSetName", RpcTarget.AllBuffered, name);
        }

        [PunRPC]
        public void RPCSetName(string name)
        {
            nameText.text = name;
        }

        // PRIVATE

        private Player GetPlayer(PhotonView view)
        {
            foreach (var player in PhotonNetwork.CurrentRoom.Players)
            {
                if (player.Value.UserId == view.Owner.UserId) return player.Value;
            }

            return null;
        }
    }
}
