using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Bolt;

public class InGameMenu : GlobalEventListener
{

    [SerializeField] private Button quitButton;
    [SerializeField] private Button allToMenu;
    [SerializeField] private GameObject inGameMenuPanel;

    private bool _menuEnabled = false;

    private void Awake()
    {
        quitButton.onClick.AddListener(QuitMatch);
        allToMenu.onClick.AddListener(AllToMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _menuEnabled = !_menuEnabled;
            inGameMenuPanel.SetActive(_menuEnabled);
        }
    }

    private void QuitMatch()
    {
        Debug.Log("Quit Match");
        if (BoltNetwork.IsServer)
            StartCoroutine(HostDisconectLastSecurity());
        else if (BoltNetwork.IsClient)
            BoltLauncher.Shutdown();
    }

    private void AllToMenu()
    {
        Debug.Log("AllToMenu");
        var DisconnectAllPlayers = AllPlayersToMenu.Create();
        DisconnectAllPlayers.Send();
    }

    public override void OnEvent(AllPlayersToMenu DisconnectAllPlayers)
    {
        QuitMatch();
    }

    public override void BoltShutdownBegin(AddCallback registerDoneCallback)
    {
        BoltLog.Warn("Bolt is shutting down");

        SceneManager.LoadScene("Menu");

        registerDoneCallback(() =>
        {
            BoltLog.Warn("Bolt is down");
        });
    }


    IEnumerator HostDisconectLastSecurity()
    {
        yield return new WaitForSeconds(1.5f);
        BoltLauncher.Shutdown();
    }

    /*
    public override void BoltShutdownBegin(AddCallback registerDoneCallback)
    {

        if (BoltNetwork.IsServer)
        {
            Debug.Log("1");
            SceneManager.LoadScene("Menu");
        }
        else if (BoltNetwork.IsClient)
        {
            Debug.Log("2");
            SceneManager.LoadScene("Menu");
        }

        registerDoneCallback(() =>
        {
           Debug.Log("Shutdown Done");
        });
    }
    */
}
