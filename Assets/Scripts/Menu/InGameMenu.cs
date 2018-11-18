using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bolt;

public class InGameMenu : MonoBehaviour {

    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject inGameMenuPanel;

    private bool _menuEnabled = false;

    private void Awake()
    {
        quitButton.onClick.AddListener(QuitMatch);
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
        BoltLauncher.Shutdown();
        BoltNetwork.LoadScene("Menu");
    }





}
