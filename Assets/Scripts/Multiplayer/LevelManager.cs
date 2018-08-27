using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HUD;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private Button quitLevel;
    [SerializeField] private Button resetLevel;

    private PhotonView test;

    private bool menuActivated;

    private void Awake()
    {
        quitLevel.onClick.AddListener(ReturnToMenu);
        resetLevel.onClick.AddListener(ResetLevel);
        test = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(Constants.EscapeMenu))
            menuActivated = !menuActivated;
        if (menuActivated)
            escapeMenu.SetActive(true);
        else
            escapeMenu.SetActive(false);
    }

    void ReturnToMenu()
    {
        PhotonNetwork.player.SetScore(0);
        PhotonNetwork.LoadLevel("Menu");
        PhotonNetwork.Disconnect();
    }

    void ResetLevel()
    {
        if (PhotonNetwork.isMasterClient)
        {
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                PhotonNetwork.player.SetScore(0);
                GameObject.Find("HUD").GetComponent<HUDUpdater>().UpdatePlayerList();
            }
            test.RPC("RPCResetLevel", PhotonTargets.All);
        }
    }
    [PunRPC]
    void RPCResetLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        PhotonNetwork.LoadLevel(activeScene.name);
    }
}
