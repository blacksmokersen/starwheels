using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Common.SplashScreen
{
    [DisallowMultipleComponent]
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

        private bool _loadMainMenu = false;

        // CORE

        private void Awake()
        {
            _playerCurrentFrame = _videoPlayer.frame;
            _playerFrameCount = System.Convert.ToInt64(_videoPlayer.frameCount);
        }

        private void Start()
        {
            StartCoroutine(LoadMenuScene());
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

            TriggerEpilepsiePanelFadeOut();
            yield return new WaitForSeconds(1.5f);
            _loadMainMenu = true;
        }

        private void TriggerEpilepsiePanelFadeIn()
        {
            _epilepsieAnimator.SetTrigger("TriggerFadeIn");
        }

        private void TriggerEpilepsiePanelFadeOut()
        {
            _epilepsieAnimator.SetTrigger("TriggerFadeOut");
        }

        private IEnumerator LoadMenuScene()
        {
            yield return new WaitForSeconds(.5f); // Delay to avoid a bug
            
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Single);
            asyncLoad.allowSceneActivation = false;

            while (!asyncLoad.isDone)
            {
                if (_loadMainMenu)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
