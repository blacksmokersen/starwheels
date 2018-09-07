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
                SetName(photonView.Owner.NickName);
                Destroy(backCamera);
            }
            SetColor(photonView.Owner.GetTeam());
        }

        private void Update()
        {
            nameText.transform.LookAt(Camera.main.transform);
        }

        // PUBLIC

        public void SetColor(PunTeams.Team team)
        {
            Debug.Log("Setting color to : " + team);
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
    }
}
