using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
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
        //PhotonNetwork.LoadLevel("Menu");
        //PhotonNetwork.Disconnect();
    }
}
