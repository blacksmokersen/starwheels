using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Photon.Pun;

public class MainMenu : MonoBehaviourPun
{
    [SerializeField] private Button soloButton;
    [SerializeField] private Button multiButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject multiplayerMenu;
    [SerializeField] private GameObject mainButtons;

    private Button[] buttons;
    private int currentIndex;
    private float oldInput;

    private void Awake()
    {
        buttons = new Button[4] { soloButton, multiButton, optionsButton, quitButton };

        soloButton.onClick.AddListener(Solo);
        multiButton.onClick.AddListener(Multi);
        quitButton.onClick.AddListener(Quit);
    }

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    private void Multi()
    {
        multiplayerMenu.SetActive(true);
        mainButtons.SetActive(false);
    }

    private void Solo()
    {
        Debug.Log("Launching Solo mode");
        PhotonNetwork.OfflineMode = true;
        PhotonNetwork.CreateRoom("Solo");
        SceneManager.LoadScene(2);
    }

    private void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
