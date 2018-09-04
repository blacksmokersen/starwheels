using UnityEngine;
using UnityEngine.UI;
using Photon;

public class RoomPlayer : PunBehaviour
{
    [SerializeField] private Text playerNameText;

    private void Awake()
    {
        playerNameText.text = photonView.owner.NickName;
        SetTransform();
    }

    public void SetTeam(PunTeams.Team team)
    {
        Debug.Log("Setting team : " + team);
        photonView.RPC("RPCChangeTeam", PhotonTargets.AllBuffered, team);
    }

    [PunRPC]
    private void RPCChangeTeam(PunTeams.Team team)
    {
        switch (team)
        {
            case PunTeams.Team.blue:
                GetComponent<Image>().color = new Color(0.3f, 0.8f, 1f);
                break;
            case PunTeams.Team.red:
                GetComponent<Image>().color = new Color(1, 0.3f, 0.3f);
                break;
            default:
                break;
        }
    }

    public void SetTransform()
    {
        photonView.RPC("RPCSetTransform", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    private void RPCSetTransform()
    {
        var playerList = GameObject.Find("PlayersList").transform;
        gameObject.transform.SetParent(playerList, true);
    }
}
