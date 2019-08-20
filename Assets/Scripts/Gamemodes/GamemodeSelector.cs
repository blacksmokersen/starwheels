using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class GamemodeSelector : GlobalEventListener
{
    [SerializeField] private GameObject _freeForAllGamemode;
    [SerializeField] private GameObject _teamBattleGamemode;

    private GameSettings _gameSettings;

    //CORE

    private void Awake()
    {
        _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
    }

    // BOLT

    public override void OnEvent(OnAllPlayersInGame evnt)
    {
        if (_gameSettings.Gamemode == Constants.Gamemodes.FFA)
        {
            _freeForAllGamemode.gameObject.SetActive(true);
        }
        else if (_gameSettings.Gamemode == Constants.Gamemodes.Battle)
        {
            _teamBattleGamemode.gameObject.SetActive(true);
        }
    }

    // PUBLIC

    public string GetActiveGamemode()
    {
        return _gameSettings.Gamemode;
    }
}
