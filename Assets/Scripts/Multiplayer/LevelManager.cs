using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    // TODO : Separer le menu des fonctions
    //[SerializeField] private GameObject escapeMenu;
    [SerializeField] private Button quitLevel;
    [SerializeField] private Button resetLevel;

    private bool menuActivated = false;

    // CORE

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

        if(quitLevel) quitLevel.onClick.AddListener(ReturnToMenu);
        if(resetLevel) resetLevel.onClick.AddListener(ResetLevel);
    }

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Input.EscapeMenu))
            menuActivated = !menuActivated;
        /*
        if (menuActivated)
            escapeMenu.SetActive(true);
        else
            escapeMenu.SetActive(false);
        */
    }

    // PUBLIC

    public void ResetLevel()
    {
        if (BoltNetwork.isServer)
        {
            var roomToken = FindObjectOfType<Multiplayer.SpawnAssigner>().RoomInfoToken;

            if(roomToken != null)
                BoltNetwork.LoadScene(SceneManager.GetActiveScene().name, roomToken);
            else
                BoltNetwork.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    // PRIVATE

    private void ReturnToMenu()
    {
        BoltLauncher.Shutdown();
        SceneManager.LoadScene(Constants.Scene.MainMenu);
    }
}
