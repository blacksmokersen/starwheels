using UnityEngine;
using Bolt;

namespace Gamemodes.Totem
{
    public class WallCollisionHandler : EntityBehaviour<ITotemWallState>
    {
        [Header("Data")]
        public int RemainingLives;
        public Team OwnerTeam;

        [Header("Events")]
        public IntEvent OnWallHit;
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
            if (OnWallHit != null)
            {
                OnWallHit.Invoke(state.Lives); // Local event
            }

            if (entity.isAttached && entity.isOwner)
            {
                state.Lives--;

                foreach (var totemSpawn in FindObjectsOfType<TotemSpawner>())
                {
                    if (OwnerTeam == Team.Red && totemSpawn.RespawnSide == TotemSpawner.Side.Red)
                    {
                        totemSpawn.RespawnTotem();
                    }
                    else if (OwnerTeam == Team.Blue && totemSpawn.RespawnSide == TotemSpawner.Side.Blue)
                    {
                        totemSpawn.RespawnTotem();
                    }
                }

                TotemWallHit totemWallHitEvent = TotemWallHit.Create();
                totemWallHitEvent.Team = (int) OwnerTeam;
                totemWallHitEvent.Send();

                if (state.Lives <= 0)
                {
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
