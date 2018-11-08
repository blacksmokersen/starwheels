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
            if (OnLifeLost != null) OnLifeLost.Invoke(state.Lives);

            if (entity.isControllerOrOwner)
            {
                state.Lives--;

                if(state.Lives <= 0)
                {
                    var winningTeam = Team.Blue;

                    GameOver gameOverEvent = GameOver.Create();
                    gameOverEvent.WinningTeam = TeamsColors.GetColorFromTeam(winningTeam);
                    gameOverEvent.Send();

                    if (OnWallDestroyed != null) OnWallDestroyed.Invoke(winningTeam);

                    BoltNetwork.Destroy(gameObject);
                }
            }
        }
    }
}
