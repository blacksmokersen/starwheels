using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Gamemodes.Totem
{
    public class OnKartTotemEventListener : GlobalEventListener
    {
        [Header("Bolt Entity")]
        [SerializeField] private BoltEntity _kartEntity;

        [Header("Events")]
        public UnityEvent OnTotemGet;
        public UnityEvent OnTotemLost;

        private IKartState _kartState;

        // BOLT

        public override void OnEvent(TotemPicked evnt)
        {
            if (evnt.NewOwnerID == GetKartState().OwnerID)
            {
                OnTotemGet.Invoke();
            }
            else if (evnt.OldOwnerID == GetKartState().OwnerID)
            {
                OnTotemLost.Invoke();
            }
        }

        public override void OnEvent(TotemThrown evnt)
        {
            if (evnt.OwnerID == GetKartState().OwnerID)
            {
                OnTotemLost.Invoke();
            }
        }

        // PRIVATE

        private IKartState GetKartState()
        {
            if (_kartState != null)
            {
                return _kartState;
            }
            else
            {
                _kartEntity.TryFindState<IKartState>(out _kartState);
                return _kartState;
            }
        }
    }
}
