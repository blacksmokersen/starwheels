using UnityEngine;
using Gamemodes;

namespace Audio
{
    public class GameModeSounds : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource endGameSource;
        [SerializeField] private AudioSource introSource;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip winAudioClip;
        [SerializeField] private AudioClip loseAudioClip;

        // PUBLIC

        public void PlayIntroSound()
        {
            introSource.Play();
        }

        public void GameEndSound(Team winningTeam)
        {
            var myTeam = Multiplayer.Player.Me.Team;
            if (myTeam == winningTeam)
            {
                PlayVictorySound();
            }
            else
            {
                PlayLoseSound();
            }
        }

        // PRIVATE

        private void PlayVictorySound()
        {
            endGameSource.clip = winAudioClip;
            endGameSource.Play();
        }

        private void PlayLoseSound()
        {
            endGameSource.clip = loseAudioClip;
            endGameSource.Play();
        }
    }
}
