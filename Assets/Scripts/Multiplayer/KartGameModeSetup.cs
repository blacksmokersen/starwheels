using UnityEngine;
using UnityEngine.Events;

namespace Gamemodes
{
    public class KartGameModeSetup : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent OnTotemModeSet;
        public UnityEvent OnFFAModeSet;
        public UnityEvent OnBattleModeSet;

        [SerializeField] private GameObject _totemSpecifics;
        [SerializeField] private GameObject _battleSpecifics;

        private GameSettings _gameSettings;

        // CORE

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
        }

        private void Start()
        {
            switch (_gameSettings.Gamemode)
            {
                case Constants.Gamemodes.Battle:
                    _battleSpecifics.SetActive(true);
                    if (OnBattleModeSet != null)
                    {
                        OnBattleModeSet.Invoke();
                    }
                    break;
                case Constants.Gamemodes.FFA:
                    _battleSpecifics.SetActive(true);
                    if (OnFFAModeSet != null)
                    {
                        OnFFAModeSet.Invoke();
                    }
                    break;
                case Constants.Gamemodes.Totem:
                    _totemSpecifics.SetActive(true);
                    if (OnTotemModeSet != null)
                    {
                        OnTotemModeSet.Invoke();
                    }
                    break;
                default:
                    Debug.LogError("Gamemode unknown !");
                    break;
            }
        }
    }
}
