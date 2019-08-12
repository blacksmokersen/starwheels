using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class KillStreakSounds : GlobalEventListener
{
    [SerializeField] private float _killStreakTimer;
    [SerializeField] private AudioClip[] _killStreakAudioclips;

    private AudioSource _audioSource;
    private Dictionary<int, int> PlayerCumulativeScoreEntries = new Dictionary<int, int>();
    private Dictionary<int, float> PlayerTimer = new Dictionary<int, float>();
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
        {
            RemoveFromAllDictionnaries(evnt.VictimID);
        }

        if (PlayerCumulativeScoreEntries.ContainsKey(evnt.KillerID))
        {
            AddCumulativeScore(evnt.KillerID);
            CheckCumulativeScore(evnt.KillerID);
        }
        else
        {
            PlayerCumulativeScoreEntries.Add(evnt.KillerID, 1);
            PlayerTimer.Add(evnt.KillerID, Time.time);
        }
    }

    //PRIVATE

    private void CheckCumulativeScore(int playerEntry)
    {
        if (PlayerCumulativeScoreEntries[playerEntry] >= 2 && Time.time - PlayerTimer[playerEntry] <= _killStreakTimer)
        {
            //  Debug.LogError("Set new Time - LastHit Was : " + (Time.time - PlayerTimer[playerEntry]));
            PlayCumulativeAudioClip(PlayerCumulativeScoreEntries[playerEntry] - 2);
            PlayerTimer[playerEntry] = Time.time;
        }
        else if(Time.time - PlayerTimer[playerEntry] > _killStreakTimer)
        {
         //   Debug.LogError("RemoveFromDictionaries - LastHit Was : " + (Time.time - PlayerTimer[playerEntry]));
            RemoveFromAllDictionnaries(playerEntry);
        }
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

    private void RemoveFromAllDictionnaries(int playerEntry)
    {
        PlayerCumulativeScoreEntries.Remove(playerEntry);
        PlayerTimer.Remove(playerEntry);
    }
}
