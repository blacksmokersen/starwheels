using UnityEngine;
using Bolt;

public class LoadingScreen : GlobalEventListener
{
    [SerializeField] private GameObject _inGameMenu;
    [SerializeField] private GameObject _mapCamera;

    private bool _playersAreReady = false;

    //CORE

    private void Awake()
    {
        if (!BoltNetwork.IsConnected) // Used for In-Editor tests
        {
            gameObject.SetActive(false);
            _inGameMenu.SetActive(true);
        }
    }

    //BOLT

    public override void OnEvent(OnAllPlayersInGame evnt)
    {
        Debug.LogError("OnAllPlayersInGame INVOKE");
        _mapCamera.GetComponent<Animator>().enabled = true;
        _inGameMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}



