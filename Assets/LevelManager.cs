using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private Button quitLevel;
    [SerializeField] private Button option;

    private bool menuActivated;

    private void Awake()
    {
        quitLevel.onClick.AddListener(ReturnToMenu);
        option.onClick.AddListener(ShowOptions);
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
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("Menu");
    }

    void ShowOptions()
    {

    }
}
