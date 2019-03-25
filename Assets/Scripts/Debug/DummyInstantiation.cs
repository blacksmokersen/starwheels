using UnityEngine;
using Multiplayer;
using Bolt;

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
        [SerializeField] private BoltEntity _dummy;

        // CORE

        private void Update()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled)
            {
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    InstantiateKart(_dummy);
                }
            }
        }

        // PRIVATE

        private void InstantiateKart(BoltEntity kart)
        {
            var dummy = BoltNetwork.Instantiate(kart, transform.position, transform.rotation);
            dummy.GetState<IKartState>().Team = Team.Red.ToString();
            dummy.GetState<IKartState>().Nickname = "Dummy";
            dummy.GetState<IKartState>().OwnerID = -2;
            dummy.ReleaseControl();
        }
    }
}
