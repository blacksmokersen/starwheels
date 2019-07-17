using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bolt;

public class LoadingScreen : GlobalEventListener
{

    [HideInInspector] public string SceneToLoad;


    private bool _playersAreReady = false;




    //CORE

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

    /*
    public void StartLoadingGameScene(string sceneName)
    {
        SceneToLoad = sceneName;
        StartCoroutine(LoadGameScene());
    }
    */

    //PRIVATE

    /*
private IEnumerator LoadGameScene()
{
    Debug.Log("Starting MainMenu load.");

    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Single);
    asyncLoad.allowSceneActivation = false;

    while (!asyncLoad.isDone)
    {
        yield return null;
    }

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
    }
    */
}


