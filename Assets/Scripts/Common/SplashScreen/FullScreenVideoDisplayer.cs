using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Common.UI
{
    public class FullScreenVideoDisplayer : MonoBehaviour
    {
        [Header("Video Settings")]
        [SerializeField] private VideoClip _videoClip;
        [SerializeField] private VideoPlayer _videoPlayer;

    }
}
