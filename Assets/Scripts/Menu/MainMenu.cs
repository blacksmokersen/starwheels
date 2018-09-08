using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    private int currentIndex;
    private float oldInput;

    private void Awake()
    {
        soloButton.onClick.AddListener(Solo);
        multiButton.onClick.AddListener(Multi);
        quitButton.onClick.AddListener(Quit);
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
        SceneManager.LoadScene(Constants.Scene.FortBlock);
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
