using System.Collections;
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
        [SerializeField] private GameObject _videoTextureObject;
        [SerializeField] private long _minimumFrameBeforeSkipping;

        private long _playerCurrentFrame;
        private long _playerFrameCount;
        private bool _videoFinished = false;

        [Header("Epilepsie")]
        [SerializeField] private GameObject _epilepsiePanel;
        [SerializeField] private Animator _epilepsieAnimator;

        private bool _epilepsiePanelShowing = true;
        private bool _canSkipEpilepsiePanel = false;

        // CORE

        private void Awake()
        {
            _playerCurrentFrame = _videoPlayer.frame;
            _playerFrameCount = System.Convert.ToInt64(_videoPlayer.frameCount);
        }

        private void Update()
        {
            _playerCurrentFrame = _videoPlayer.frame;

            if (CanSkipVideo() && !_videoFinished)
            {
                if (Input.anyKeyDown)
                {
                    ShowEpilepsiePanel();
                }
                else if (VideoIsOver())
                {
                    ShowEpilepsiePanel();
                }
            }

            if (_canSkipEpilepsiePanel && Input.anyKeyDown)
            {
                _epilepsiePanelShowing = false;
            }
        }

        // PRIVATE

        private bool CanSkipVideo()
        {
            return _playerCurrentFrame > _minimumFrameBeforeSkipping;
        }

        private bool VideoIsOver()
        {
            return _playerCurrentFrame >= _playerFrameCount;
        }

        private void DisableVideo()
        {
            _videoPlayer.Pause();
            _videoTextureObject.SetActive(false);
            _videoFinished = true;
        }

        private void ShowEpilepsiePanel()
        {
            DisableVideo();
            StartCoroutine(ShowEpilepsiePanelRoutine());
        }

        private IEnumerator ShowEpilepsiePanelRoutine()
        {
            _epilepsiePanel.SetActive(true);

            TriggerEpilepsiePanelFadeIn();
            yield return new WaitForSeconds(1f);


            var timer = 0f;
            while (_epilepsiePanelShowing)
            {
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    _canSkipEpilepsiePanel = true;
                }
                if (timer >= 3f)
                {
                    _epilepsiePanelShowing = false;
                    break;
                }
            }
            Debug.Log("Finished showing epilepsie panel.");

            TriggerEpilepsiePanelFadeOut();
            yield return new WaitForSeconds(1.2f);

            LoadMenuScene();
        }

        private void TriggerEpilepsiePanelFadeIn()
        {
            _epilepsieAnimator.SetTrigger("TriggerFadeIn");
        }

        private void TriggerEpilepsiePanelFadeOut()
        {
            _epilepsieAnimator.SetTrigger("TriggerFadeOut");
        }

        private void LoadMenuScene()
        {
            Debug.Log("Loading MainMenu.");
            SceneManager.LoadScene(_nextSceneName, LoadSceneMode.Single);
        }
    }
}
