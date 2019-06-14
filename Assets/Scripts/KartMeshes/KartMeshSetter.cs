using UnityEngine;
using UnityEngine.Events;
using Multiplayer;
using Bolt;

namespace SW.Customization
{
    [DisallowMultipleComponent]
    public class KartMeshSetter : GlobalEventListener
    {
        [Header("Events")]
        public UnityEvent OnKartSwitched;

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
        }

        private void Start()
        {
            SetKartWithLocalSettings();
        }

        // BOLT

        public override void OnEvent(PlayerReady evnt)
        {
            if (!evnt.Entity.IsOwner && evnt.Entity == GetComponentInParent<BoltEntity>())
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
                    _playerSettings.KartIndex = i;

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
            SetKart((_currentIndex - 1 +_karts.Length ) % _karts.Length);
        }

        [ContextMenu("Switch Kart")]
        public void SetKartWithLocalSettings()
        {
            SetKart(_playerSettings.KartIndex);
        }
    }
}
