using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class EventsSoundManager : GlobalEventListener
{
    [SerializeField] private AudioClip[] _killStreakAudioclips;

    private AudioSource _audioSource;
    private Dictionary<int, int> PlayerCumulativeScoreEntries = new Dictionary<int, int>();
    private bool _isFirstBloodOccured = false;

    //CORE

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    //BOLT

    public override void OnEvent(PlayerHit evnt)
    {
        if (PlayerCumulativeScoreEntries.Count == 0 && !_isFirstBloodOccured)
        {
            _audioSource.clip = _killStreakAudioclips[6];
            _audioSource.Play();
            _isFirstBloodOccured = true;
        }

        if (PlayerCumulativeScoreEntries.ContainsKey(evnt.VictimID))
            PlayerCumulativeScoreEntries.Remove(evnt.VictimID);

        if (PlayerCumulativeScoreEntries.ContainsKey(evnt.KillerID))
        {
            AddCumulativeScore(evnt.KillerID);
            CheckCumulativeScore(evnt.KillerID);
        }
        else
            PlayerCumulativeScoreEntries.Add(evnt.KillerID, 1);
    }

    //PRIVATE

    private void CheckCumulativeScore(int playerEntry)
    {
        if (PlayerCumulativeScoreEntries[playerEntry] >= 2)
            PlayCumulativeAudioClip(PlayerCumulativeScoreEntries[playerEntry] - 2);
    }

    private void AddCumulativeScore(int playerEntry)
    {
        if (PlayerCumulativeScoreEntries[playerEntry] <= 6)
            PlayerCumulativeScoreEntries[playerEntry] += 1;
    }

    private void PlayCumulativeAudioClip(int cumulativeScore)
    {
        _audioSource.clip = _killStreakAudioclips[cumulativeScore];
        _audioSource.Play();
    }
}
