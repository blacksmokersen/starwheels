using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Common.SplashScreen
{
    public class LoadSceneOnInput : MonoBehaviour
    {
        [SerializeField] private string _nextSceneName;

        [Header("Video")]
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private long _minimumFrameBeforeSkipping;

        private long _playerCurrentFrame;
        private long _playerFrameCount;

        // CORE

        private void Awake()
        {
            _playerCurrentFrame = _videoPlayer.frame;
            _playerFrameCount = System.Convert.ToInt64(_videoPlayer.frameCount);
        }

        private void Update()
        {
            _playerCurrentFrame = _videoPlayer.frame;

            if (CanSkipVideo())
            {
                if (Input.anyKeyDown)
                {
                    Debug.Log(_playerCurrentFrame);
                    LoadScene();
                }
                else if (VideoIsOver())
                {
                    LoadScene();
                }
            }
        }

        // PRIVATE

        private void LoadScene()
        {
            SceneManager.LoadScene(_nextSceneName);
        }

        private bool CanSkipVideo()
        {
            return _playerCurrentFrame > _minimumFrameBeforeSkipping;
        }

        private bool VideoIsOver()
        {
            return _playerCurrentFrame >= _playerFrameCount;
        }
    }
}
