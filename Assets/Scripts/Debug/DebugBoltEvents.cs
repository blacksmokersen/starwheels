using UnityEngine;
using Multiplayer.Teams;
using Multiplayer;
using Gamemodes;
using TMPro;

namespace SW.DebugUtils
{
    public class DebugBoltEvents : GamemodeBase
    {
        [Header("UI Elements")]
        [SerializeField] private TMP_InputField _redScoreInput;
        [SerializeField] private TMP_InputField _blueScoreInput;

        private PlayerInfo _playerInfo;

        public void TriggerGameOverEvent()
        {
            GameOver gameOverEvent = GameOver.Create();
            gameOverEvent.WinningTeam = (int)Team.Blue;
            gameOverEvent.Send();
        }

        public void TriggerGameReadyEvent()
        {
            GameReady gameReadyEvent = GameReady.Create();
            gameReadyEvent.Send();
        }

        public void LobbyCoutndownEvent()
        {
            LobbyCountdown countdownEvent = LobbyCountdown.Create();
            countdownEvent.Time = 0;
            countdownEvent.Send();
        }

        public void TriggerRedScoreIncreaseEvent()
        {
            ScoreIncreased scoreIncreasedEvent = ScoreIncreased.Create();
            scoreIncreasedEvent.Team = (int)Team.Red;
            scoreIncreasedEvent.Score = int.Parse(_redScoreInput.text);
            scoreIncreasedEvent.Send();
        }

        public void TriggerBlueScoreIncreaseEvent()
        {
            ScoreIncreased scoreIncreasedEvent = ScoreIncreased.Create();
            scoreIncreasedEvent.Team = (int)Team.Blue;
            scoreIncreasedEvent.Score = int.Parse(_blueScoreInput.text);
            scoreIncreasedEvent.Send();
        }

        public void TriggerPlayerHitEvent()
        {

            GameObject kart = SWExtensions.KartExtensions.GetKartWithID(SWMatchmaking.GetMyBoltId());

            PlayerHit playerHitEvent = PlayerHit.Create();
            playerHitEvent.VictimEntity = kart.GetComponent<BoltEntity>();

            playerHitEvent.KillerID = kart.GetComponent<PlayerInfo>().OwnerID;
            playerHitEvent.KillerName = kart.GetComponent<PlayerInfo>().name;
            playerHitEvent.KillerTeam = (int)kart.GetComponent<PlayerInfo>().Team;
         //   playerHitEvent.Item = _ownership.Label;
            playerHitEvent.VictimID = kart.GetComponent<PlayerInfo>().OwnerID;
            playerHitEvent.VictimName = kart.GetComponent<PlayerInfo>().name;
            playerHitEvent.VictimTeam = (int)kart.GetComponent<PlayerInfo>().Team;




            playerHitEvent.Send();





        }

        public void TriggerPlayerQuitEvent()
        {
            PlayerQuit playerQuitEvent = PlayerQuit.Create();
            playerQuitEvent.PlayerID = SWMatchmaking.GetMyBoltId();
            playerQuitEvent.Send();
        }

        public void IncreaseScore()
        {
            _playerInfo = FindObjectOfType<PlayerInfo>();
            IncreaseScore(_playerInfo.Team, 1);
            SendScoreIncreasedEvent(_playerInfo.Team);
        //    CheckScore();
            /*
            PlayerStatUpdate playerKillCountUpdate = PlayerStatUpdate.Create();
            playerKillCountUpdate.StatName = Constants.PlayerStats.KillCountName;
            playerKillCountUpdate.PlayerID = _playerInfo.OwnerID;
            playerKillCountUpdate.StatValue ++;
            playerKillCountUpdate.Send();
            */
        }
    }
}
