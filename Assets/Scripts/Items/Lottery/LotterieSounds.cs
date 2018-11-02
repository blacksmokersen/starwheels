using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotterieSounds : MonoBehaviour {

    public AudioSource LotteryAudio;
    public AudioSource GetItemAudio;

    public void PlayLotteryAudio()
    {
        LotteryAudio.Play();
    }

    public void StopLotteryAudio()
    {
        LotteryAudio.Stop();
    }

    public void PlayGetItemAudio()
    {
        GetItemAudio.Play();
    }

}
