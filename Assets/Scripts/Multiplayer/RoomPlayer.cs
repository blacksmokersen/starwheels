using UnityEngine;
using UnityEngine.UI;

public class RoomPlayer : MonoBehaviour
{
    public string PlayerNickname;

    [SerializeField] private Text playerNameText;

    public void SetPlayer(PhotonPlayer player)
    {
        PlayerNickname = player.NickName;
        playerNameText.text = player.NickName;
    }

    public void SetTeam(PunTeams.Team team)
    {
        PhotonNetwork.player.SetTeam(team);
        Debug.Log("Setting team : " + team);
        PhotonView photonView = GetComponent<PhotonView>();
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
}
