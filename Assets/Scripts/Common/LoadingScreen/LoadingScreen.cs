using UnityEngine;
using System.Collections;
using Bolt;

public class LoadingScreen : GlobalEventListener
{
    [Header("Settings")]
    [SerializeField] private float _loadingMinDuration;

    [Header("UI Elements")]
    [SerializeField] private GameObject _inGameMenu;
    [SerializeField] private GameObject _mapCamera;
    [SerializeField] private GameObject _abilityPickPanel;

    private bool _playersAreReady = false;

    //CORE

    private void Awake()
    {
        if (!BoltNetwork.IsConnected) // Used for In-Editor tests
        {
            gameObject.SetActive(false);
            _inGameMenu.SetActive(true);
        }
        else
            _abilityPickPanel.SetActive(false);
    }

    //BOLT

    public override void OnEvent(OnAllPlayersInGame evnt)
    {
        //Debug.LogError("OnAllPlayersInGame INVOKE");
        _inGameMenu.SetActive(true);
        StartCoroutine(LoadingMinimumUpTime(_loadingMinDuration));
    }

    private IEnumerator LoadingMinimumUpTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        _mapCamera.GetComponent<Animator>().enabled = true;
        _abilityPickPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}



