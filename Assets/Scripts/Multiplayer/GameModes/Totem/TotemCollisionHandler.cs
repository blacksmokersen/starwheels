using UnityEngine;
using Bolt;
using Multiplayer.Teams;

namespace GameModes.Totem
{
    public class TotemCollisionHandler : EntityBehaviour<ITotemWallState>
    {
        [Header("Data")]
        public int RemainingLives;
        public Team OwnerTeam;

        [Header("Events")]
        public IntEvent OnLifeLost;
        public TeamEvent OnWallDestroyed;

        public override void ControlGained()
        {
            if (entity.isControllerOrOwner) state.Lives = RemainingLives;
        }

        // MONOBEHAVIOUR

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Constants.Tag.Totem))
            {
                LoseLife();
            }
        }

        // PRIVATE

        private void LoseLife()
        {
            if (OnLifeLost != null) OnLifeLost.Invoke(state.Lives); // Local event

            if (entity.isControllerOrOwner)
            {
                state.Lives--;

                FindObjectOfType<TotemSpawner>().RespawnTotem();

                TotemWallHit totemWallHitEvent = TotemWallHit.Create();
                totemWallHitEvent.Team = OwnerTeam.GetColor();
                totemWallHitEvent.Send();

                if (state.Lives <= 0)
                {
                    GameOver gameOverEvent = GameOver.Create();
                    gameOverEvent.WinningTeam = OwnerTeam.OppositeTeam().GetColor();
                    gameOverEvent.Send();

                    if (OnWallDestroyed != null) OnWallDestroyed.Invoke(OwnerTeam);

                    BoltNetwork.Destroy(gameObject);
                }
            }
        }
    }
}
