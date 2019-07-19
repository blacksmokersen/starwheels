using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bolt;
using SW.Matchmaking;
using Multiplayer;

public class LoadingScreen : GlobalEventListener
{


    [HideInInspector] public string SceneToLoad;

    [SerializeField] private LobbyData _lobbyData;

    private bool _playersAreReady = false;




    //CORE

    private void Awake()
    {
        if (!BoltNetwork.IsConnected) // Used for In-Editor tests
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
      //  SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);
      //  StartLoadingGameScene(_lobbyData.ChosenMapName);
    }

    /*
    private void Update()
    {
        if (_playersAreReady)
        {
            SceneManager.UnloadSceneAsync("LoadingScene");
            _playersAreReady = false;
           // StartGame();
        }
    }
    */

    //BOLT

    public override void OnEvent(OnAllPlayersInGame evnt)
    {
        Debug.LogError("OnAllPlayersInGame INVOKE");
        gameObject.SetActive(false);

       // SceneManager.UnloadSceneAsync("LoadingScene");
        // _playersAreReady = true;
    }


    //PUBLIC


    public void StartLoadingGameScene(string sceneName)
    {
       // SceneToLoad = sceneName;
      //  StartCoroutine(StartGameIn(3));
      //  StartCoroutine(LoadGameScene());
    }


    //PRIVATE

    private void StartGame()
    {
        var roomToken = new RoomProtocolToken() { Gamemode = _lobbyData.ChosenGamemode, PlayersCount = _lobbyData.CurrentPlayers};
        BoltNetwork.LoadScene(_lobbyData.ChosenMapName, roomToken);
    }


        private IEnumerator StartGameIn(float timer)
    {
        yield return new WaitForSeconds(timer);

        var roomToken = new RoomProtocolToken() { Gamemode = _lobbyData.ChosenGamemode, PlayersCount = _lobbyData.CurrentPlayers };

        BoltNetwork.LoadScene(_lobbyData.ChosenMapName, roomToken);

    }

    private IEnumerator LoadGameScene()
    {
        Debug.Log("Starting MainMenu load.");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            Debug.Log("1");
            if (asyncLoad.progress >= 0.9f)
            {
              //  asyncLoad.allowSceneActivation = true;
                if (BoltNetwork.IsServer)
                {
                    Debug.Log("2");
                    _playersAreReady = true;
                    /*
                    var roomToken = new RoomProtocolToken() { Gamemode = _lobbyData.ChosenGamemode };
                    BoltNetwork.LoadScene(_lobbyData.ChosenMapName, roomToken);
                    */
                }
                Debug.Log("3");
            }
            yield return null;
        }
        Debug.Log("4");
        /*
        if (BoltNetwork.IsServer)
        {
            var roomToken = new RoomProtocolToken() { Gamemode = _lobbyData.ChosenGamemode };
            BoltNetwork.LoadScene(_lobbyData.ChosenMapName, roomToken);
        }
        */

        /*
        if (asyncLoad.isDone && _playersAreReady)
        {
            asyncLoad.allowSceneActivation = true;
        }
        */

        /*
        while (!asyncLoad.isDone)
        {
            if (_loadMainMenu)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
            */
    }
}



