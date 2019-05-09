using UnityEngine;
using Multiplayer;
using Bolt;
using Common;

namespace SW.DebugUtils
{
    public class DummyInstantiation : EntityBehaviour<IKartState>, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Karts")]
        [SerializeField] private BoltEntity _dummyPrefab;

        private PlayerSettings _playerSettings;
        private BoltEntity _lastInstantiatedDummy;
        private bool _hasControlOfDummy = false;

        // CORE

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);
        }

        private void Update()
        {
            if (entity.isAttached && entity.isOwner)
            {
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled)
            {
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    InstantiateKart(_dummyPrefab);
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    SwitchControlOfLastDummy();
                }
            }
        }

        // PRIVATE

        private void InstantiateKart(BoltEntity kart)
        {
            var spawnPosition = transform.position + (5 * transform.forward);
            _lastInstantiatedDummy = BoltNetwork.Instantiate(kart, spawnPosition, transform.rotation);
            _lastInstantiatedDummy.GetState<IKartState>().Team = (int)_playerSettings.ColorSettings.TeamEnum.OppositeTeam();
            _lastInstantiatedDummy.GetState<IKartState>().Nickname = "Dummy";
            _lastInstantiatedDummy.GetState<IKartState>().OwnerID = -2;
            ReleaseControlOfDummy();
        }

        private void SwitchControlOfLastDummy()
        {
            if (_hasControlOfDummy)
            {
                ReleaseControlOfDummy();
            }
            else
            {
                TakeControlOfDummy();
            }
        }

        private void TakeControlOfDummy()
        {
            entity.ReleaseControl();
            GetComponentInParent<ControllableDisabler>().DisableAllInChildren();
            Enabled = true;
            _lastInstantiatedDummy.TakeControl();
            _lastInstantiatedDummy.GetComponent<ObserversSetup>().SetObservers();
            _lastInstantiatedDummy.GetComponent<ControllableDisabler>().EnableAllInChildren();

            _hasControlOfDummy = true;
        }

        private void ReleaseControlOfDummy()
        {
            entity.TakeControl();
            entity.GetComponentInParent<ControllableDisabler>().EnableAllInChildren();
            GetComponentInParent<ObserversSetup>().SetObservers();
            if (_lastInstantiatedDummy)
            {
                _lastInstantiatedDummy.GetComponent<ControllableDisabler>().DisableAllInChildren();
                _lastInstantiatedDummy.ReleaseControl();
            }

            _hasControlOfDummy = false;
        }
    }
}
