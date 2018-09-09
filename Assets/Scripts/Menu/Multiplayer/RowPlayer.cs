using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RowPlayer : MonoBehaviour {

    [SerializeField] private Text playerNameText;

    private Player _player;

    // CORE

    // PUBLIC

    public int GetPlayerId()
    {
        return _player.ActorNumber;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
        SetNickName(player.NickName);
        SetTeam(player.GetTeam());
    }

    public void SetNickName(string name)
    {
        playerNameText.text = name;
    }

    public void SetTeam(PunTeams.Team team)
    {
        switch (team)
        {
            case PunTeams.Team.blue:
                GetComponent<Image>().color = new Color(0.3f, 0.8f, 1f, 0.5f);
                break;
            case PunTeams.Team.red:
                GetComponent<Image>().color = new Color(1f, 0.3f, 0.3f, 0.5f);
                break;
            default:
                GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
                break;
        }
    }

    // PRIVATE
}
