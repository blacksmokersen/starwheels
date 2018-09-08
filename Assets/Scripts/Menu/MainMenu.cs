﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Photon.Pun;

public class MainMenu : MonoBehaviourPun
{
    private enum State
    {
        Main,
        Multiplayer,
        Options
    }

    [SerializeField] private Button soloButton;
    [SerializeField] private Button multiButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private Button backButton;

    private GameObject mainMenu;
    [SerializeField] private GameObject multiplayerMenu;

    private State currentState;

    private void Awake()
    {
        soloButton.onClick.AddListener(Solo);
        multiButton.onClick.AddListener(Multi);
        optionsButton.onClick.AddListener(Options);
        quitButton.onClick.AddListener(Quit);

        backButton.onClick.AddListener(Back);

        mainMenu = gameObject;
    }

    private void Main()
    {
        currentState = State.Main;
        UpdateMenu();
    }

    private void Solo()
    {
        Debug.Log("Launching Solo mode");
        PhotonNetwork.OfflineMode = true;
        PhotonNetwork.CreateRoom("Solo");
        SceneManager.LoadScene(Constants.Scene.FortBlock);
    }

    private void Multi()
    {
        currentState = State.Multiplayer;
        UpdateMenu();
    }

    private void Options()
    {
        currentState = State.Options;
        UpdateMenu();
    }

    private void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Back()
    {
        switch (currentState)
        {
            case State.Main:
                break;
            case State.Multiplayer:
                currentState = State.Main;
                break;
            case State.Options:
                currentState = State.Main;
                break;
        }

        UpdateMenu();
    }

    private void UpdateMenu()
    {
        switch (currentState)
        {
            case State.Main:
                mainMenu.SetActive(true);
                multiplayerMenu.SetActive(false);
                backButton.interactable = false;
                break;
            case State.Multiplayer:
                mainMenu.SetActive(false);
                multiplayerMenu.SetActive(true);
                backButton.interactable = true;
                break;
            case State.Options:
                backButton.interactable = true;
                break;
        }
    }
}
