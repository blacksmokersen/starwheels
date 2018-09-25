using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace Kart
{
    public class KartMultiplayerSettings : BaseKartComponent
    {
        [SerializeField] private Material redKartMaterial;
        [SerializeField] private Material blueKartMaterial;

        [SerializeField] private MeshRenderer kartRenderer;
        [SerializeField] private TMPro.TextMeshPro nameText;
        [SerializeField] private GameObject nameFrameGO;
        [SerializeField] private GameObject backCamera;

        // CORE

        private new void Awake()
        {
            base.Awake();

            photonView = GetComponent<PhotonView>();
            if (photonView.IsMine)
            {
                nameText.gameObject.SetActive(false);
                nameFrameGO.SetActive(false);
            }
            else if (PhotonNetwork.IsConnected && !photonView.IsMine)
            {
                SetName(photonView.Owner.NickName);
                SetFrameColor(photonView.Owner.GetTeam());
                Destroy(backCamera);
            }            

            SetKartColor(photonView.Owner.GetTeam());
        }

        private void Update()
        {
            nameText.transform.LookAt(Camera.main.transform);
            nameFrameGO.transform.LookAt(Camera.main.transform);
        }

        // PUBLIC

        public void SetKartColor(PunTeams.Team team)
        {
            switch (team)
            {
                case PunTeams.Team.blue:
                    kartRenderer.material = blueKartMaterial;
                    break;
                case PunTeams.Team.red:
                    kartRenderer.material = redKartMaterial;
                    break;
            }
        }

        public void SetFrameColor(PunTeams.Team team)
        {
            switch (team)
            {
                case PunTeams.Team.blue:
                    nameFrameGO.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.5f, 1f, 0.8f);
                    break;
                case PunTeams.Team.red:
                    nameFrameGO.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.2f, 0.8f);
                    break;
            }
        }

        public void SetName(string name)
        {
            nameText.text = name;
        }
    }
}
