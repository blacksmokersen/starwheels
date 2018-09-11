using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Kart;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class LevelManager : MonoBehaviourPun
{
    public static LevelManager Instance;

    // TODO : Separer le menu des fonctions
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private Button quitLevel;
    [SerializeField] private Button resetLevel;

    private bool menuActivated;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        quitLevel.onClick.AddListener(ReturnToMenu);
        resetLevel.onClick.AddListener(ResetLevel);
    }

    // PUBLIC

    public void ResetLevel()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPCResetLevel", RpcTarget.All);
        }
    }


    // PRIVATE

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

    [PunRPC]
    void RPCResetLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        PhotonNetwork.LoadLevel(activeScene.name);
    }
}
