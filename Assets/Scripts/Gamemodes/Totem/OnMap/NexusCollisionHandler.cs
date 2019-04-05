using UnityEngine;

namespace Gamemodes.Totem
{
    [DisallowMultipleComponent]
    public class NexusCollisionHandler : MonoBehaviour
    {
        [Header("Data")]
        public Team OwnerTeam;

        [Header("Events")]
        public TeamEvent OnNexusDestroyed;

        // CORE

        private void OnCollisionEnter(Collision collision)
        {
            if (BoltNetwork.IsServer && collision.gameObject.CompareTag(Constants.Tag.Totem))
            {
                SendGameOverEvent();

                if (OnNexusDestroyed != null)
                {
                    OnNexusDestroyed.Invoke(OwnerTeam);
                }
            }
        }

        // PRIVATE

        private void SendGameOverEvent()
        {
            GameOver gameOverEvent = GameOver.Create();
            gameOverEvent.WinningTeam = OwnerTeam.OppositeTeam().ToString();
            gameOverEvent.Send();
        }
    }
}
