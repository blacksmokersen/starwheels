using UnityEngine;
using Bolt;

namespace Gamemodes.Totem
{
    public class TotemCollisionHandler : EntityBehaviour<ITotemWallState>
    {
        [Header("Data")]
        public int RemainingLives;
        public Team OwnerTeam;

        [Header("Events")]
        public IntEvent OnLifeLost;
        public TeamEvent OnWallDestroyed;

        public override void Attached()
        {
            if (entity.isOwner)
            {
                state.Lives = RemainingLives;
            }
        }

        // MONOBEHAVIOUR

        private void OnCollisionEnter(Collision collision)
        {
            if (entity.isAttached && collision.gameObject.CompareTag(Constants.Tag.Totem))
            {
                LoseLife();
            }
        }

        // PRIVATE

        private void LoseLife()
        {
            if (OnLifeLost != null)
            {
                OnLifeLost.Invoke(state.Lives); // Local event
            }

            if (entity.isAttached && entity.isOwner)
            {
                state.Lives--;

                FindObjectOfType<TotemSpawner>().RespawnTotem();

                TotemWallHit totemWallHitEvent = TotemWallHit.Create();
                totemWallHitEvent.Team = OwnerTeam.ToString();
                totemWallHitEvent.Send();
                Debug.Log("Totem wall has been hit !");

                if (state.Lives <= 0)
                {
                    GameOver gameOverEvent = GameOver.Create();
                    gameOverEvent.WinningTeam = OwnerTeam.ToString();
                    gameOverEvent.Send();

                    if (OnWallDestroyed != null)
                    {
                        OnWallDestroyed.Invoke(OwnerTeam);
                    }

                    BoltNetwork.Destroy(gameObject);
                }
            }
        }
    }
}
