using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UdpKit;
#if UNITY_EDITOR
using UnityEditor;
#endif
using ExitGames.Client.Photon;

namespace Menu
{
    public class MainMenu : Bolt.GlobalEventListener
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
        [SerializeField] private MultiplayerMenu multiplayerMenu;

        private State currentState;

        // CORE

        private void Awake()
        {
            soloButton.onClick.AddListener(Solo);
            multiButton.onClick.AddListener(Multi);
            optionsButton.onClick.AddListener(Options);
            quitButton.onClick.AddListener(Quit);

            backButton.onClick.AddListener(Back);

            mainMenu = gameObject;
        }

        // PUBLIC


        public override void BoltStartDone()
        {
            if (BoltNetwork.isServer)
            {

                string matchName = "Test";
                BoltNetwork.SetServerInfo(matchName, null);
                BoltNetwork.LoadScene("RefactoTest");
            }
            Debug.Log("Gooo");
        }

        public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
        {
            Debug.LogFormat("Session list updated : {0} total sessions", sessionList.Count);
            foreach(var session in sessionList)
            {
                UdpSession photonSession = session.Value as UdpSession;
                if(photonSession.Source == UdpSessionSource.Photon)
                {
                    BoltNetwork.Connect(photonSession);
                }
            }
        }

        // PRIVATE

        private void Main()
        {
            currentState = State.Main;
            UpdateMenu();
        }

        private void Solo()
        {
            Debug.Log("Launching Solo mode");
            BoltLauncher.StartSinglePlayer();
        }

        private void Multi()
        {
            BoltLauncher.StartServer();
            currentState = State.Multiplayer;
            BoltLauncher.StartClient();
            //UpdateMenu();
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
                    multiplayerMenu.DisconnectFromPhoton();
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
                    multiplayerMenu.gameObject.SetActive(false);
                    backButton.interactable = false;
                    break;
                case State.Multiplayer:
                    mainMenu.SetActive(false);
                    multiplayerMenu.gameObject.SetActive(true);
                    backButton.interactable = true;
                    multiplayerMenu.ConnectToPhoton();
                    break;
                case State.Options:
                    backButton.interactable = true;
                    break;
            }
        }
    }
}
