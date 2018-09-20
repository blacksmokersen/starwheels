using UnityEngine;
using GameModes;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

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

        // CORE

        private void Start()
        {
            GameModeEvents.Instance.OnGameStart += PlayIntroSound;
            GameModeEvents.Instance.OnGameEnd += GameEnd;
        }

        // PRIVATE

        private void GameEnd(PunTeams.Team winningTeam)
        {
            var myTeam = PhotonNetwork.LocalPlayer.GetTeam();
            if (myTeam == winningTeam)
            {
                PlayVictorySound();
            }
            else
            {
                PlayLoseSound();
            }
        }

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

        private void PlayIntroSound()
        {
            introSource.Play();
        }
    }
}
