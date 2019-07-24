using UnityEngine;
using UnityEngine.Events;
using Multiplayer;
using Bolt;

namespace SW.Customization
{
    [DisallowMultipleComponent]
    public class KartMeshSetter : GlobalEventListener
    {
        [Header("Entity")]
        [SerializeField] private BoltEntity _entity;

        [Header("Events")]
        public UnityEvent OnKartSwitched;
        public IntEvent OnKartIndexUpdated;

        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;

        [Header("Karts")]
        public GameObject CurrentKart;
        [SerializeField] private GameObject _kartMesh0;
        [SerializeField] private GameObject _kartMesh1;
        [SerializeField] private GameObject _kartMesh2;

        private GameObject[] _karts;
        private int _currentIndex;

        // CORE

        private void Awake()
        {
            _karts = new GameObject[3] { _kartMesh0, _kartMesh1, _kartMesh2 };
            for (int i = 0; i < _karts.Length; i++)
            {
                if (_karts[i] == CurrentKart)
                {
                    _currentIndex = i;
                }
            }
        }

        private void Start()
        {
            if (_entity == null)  // Entity is null on Menu
                SetKartWithLocalSettings();
            if (_entity && _entity.IsOwner)
                SetKartWithLocalSettings();
        }

        // BOLT

        public override void OnEvent(PlayerReady evnt)
        {
            Debug.LogError(evnt.KartIndex + "111");
            if (!evnt.Entity.IsOwner && evnt.Entity == _entity)
            {
                SetKart(evnt.KartIndex);
            }
        }

        public override void OnEvent(PlayerInfoEvent evnt)
        {
            Debug.LogError(evnt.KartIndex + "222");
            if (evnt.TargetPlayerID == SWMatchmaking.GetMyBoltId() && // This event is for me
                evnt.KartEntity == _entity && // This is the targetted kart
                !evnt.KartEntity.IsOwner) // I don't own this kart
            {
                SetKart(evnt.KartIndex);
            }
        }

        // PUBLIC

        public void SetKart(int index)
        {
            for (int i = 0; i < _karts.Length; i++)
            {
                if (i == index)
                {
                    CurrentKart = _karts[i];
                    CurrentKart.SetActive(true);

                    _currentIndex = i;

                    if (OnKartSwitched != null)
                    {
                        OnKartSwitched.Invoke();
                    }
                }
                else
                {
                    _karts[i].SetActive(false);
                }
            }
        }

        public void SetNextKart()
        {
            SetKart((_currentIndex + 1) % _karts.Length);
        }

        public void SetPreviousKart()
        {
            SetKart((_currentIndex - 1 + _karts.Length) % _karts.Length);
        }

        public void SetKartWithLocalSettings()
        {
            SetKart(_playerSettings.KartIndex);
        }

        public void SetKartIndexSettings(int index)
        {
            _playerSettings.KartIndex = index;

            if (OnKartIndexUpdated != null)
            {
                OnKartIndexUpdated.Invoke(index);
            }
        }

        public void SetNextKartIndex()
        {
            SetKartIndexSettings((_playerSettings.KartIndex + 1) % _karts.Length);
        }

        public void SetPreviousKartIndex()
        {
            SetKartIndexSettings((_playerSettings.KartIndex - 1 + _karts.Length) % _karts.Length);
        }
    }
}
