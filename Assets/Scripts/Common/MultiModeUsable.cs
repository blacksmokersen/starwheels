using UnityEngine;
using Bolt;

namespace Common
{
    [DisallowMultipleComponent]
    public class MultiModeUsable : GlobalEventListener
    {
        public int Mode;

        [Header("Events")]
        public IntEvent OnModeUpdated;

        public void SetMode(int mode)
        {
            Mode = mode;
            OnModeUpdated.Invoke(Mode);
        }

        public override void OnEvent(ItemThrown evnt)
        {
            if (evnt.Entity == GetComponent<BoltEntity>())
            {
                SetMode(evnt.UsageMode);
            }
        }
    }
}
