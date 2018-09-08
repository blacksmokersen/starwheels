using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace Kart
{
    public class KartMultiplayerSettings : BaseKartComponent
    {
        [SerializeField] private Material redKartMaterial;
        [SerializeField] private Material blueKartMaterial;

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
            switch (team)
            {
                case PunTeams.Team.blue:
                    kartRenderer.material = blueKartMaterial;
                    break;
                case PunTeams.Team.red:
                    kartRenderer.material = redKartMaterial;
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
