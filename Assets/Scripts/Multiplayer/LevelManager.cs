using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Kart;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class LevelManager : MonoBehaviourPun
{
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private Button quitLevel;
    [SerializeField] private Button resetLevel;

    private bool menuActivated;

    private void Awake()
    {
        quitLevel.onClick.AddListener(ReturnToMenu);
        resetLevel.onClick.AddListener(ResetLevel);
    }

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Input.EscapeMenu))
            menuActivated = !menuActivated;
        if (menuActivated)
            escapeMenu.SetActive(true);
        else
            escapeMenu.SetActive(false);
    }

    void ReturnToMenu()
    {
        PhotonNetwork.LoadLevel("Menu");
        PhotonNetwork.Disconnect();
    }

    void ResetLevel()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPCResetLevel", RpcTarget.All);
        }
    }
    [PunRPC]
    void RPCResetLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        PhotonNetwork.LoadLevel(activeScene.name);
    }
}
