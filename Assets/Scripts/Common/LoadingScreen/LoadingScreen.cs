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

    private void Start()
    {
        StartLoadingGameScene(_lobbyData.ChosenMapName);
    }

    private void Update()
    {
        if (_playersAreReady)
        {

        }
    }


    //BOLT

    public override void OnEvent(PlayerReady evnt)
    {
        _playersAreReady = true;
    }


    //PUBLIC


    public void StartLoadingGameScene(string sceneName)
    {
        SceneToLoad = sceneName;
        StartCoroutine(LoadGameScene());
    }


    //PRIVATE


    private IEnumerator LoadGameScene()
    {
        Debug.Log("Starting MainMenu load.");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (BoltNetwork.IsServer)
        {
            var roomToken = new RoomProtocolToken() { Gamemode = _lobbyData.ChosenGamemode };
            BoltNetwork.LoadScene(_lobbyData.ChosenMapName, roomToken);
        }


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



