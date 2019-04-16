using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Photon;

namespace Knockout
{
    public class Knockout : EntityBehaviour
    {
        [Header("Is Active")]
        [SerializeField] private bool Enabled = false;

        [Header("Events")]
        public UnityEvent OnKnockout;

        public override void Attached()
        {
            if (entity.attachToken != null)
            {
                var roomToken = (RoomProtocolToken)entity.attachToken;
                Enabled = true;// roomToken.Gamemode == Constants.Gamemodes.Totem;
            }
            else
            {
                Debug.LogError("Couldn't find the attached token to set the knockout mode.");
            }
        }

        public void ApplyKnockoutState()
        {
            if (Enabled)
            {
                Debug.Log("Applying knockout state");
                OnKnockout.Invoke();
            }
        }
    }
}
