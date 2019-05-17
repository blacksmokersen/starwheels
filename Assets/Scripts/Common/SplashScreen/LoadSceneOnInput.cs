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
        [SerializeField] private long _minimumFrameBeforeSkipping;

        private long _playerCurrentFrame;
        private long _playerFrameCount;

        [Header("Epilepsie")]
        [SerializeField] private GameObject _epilepsiePanel;
        [SerializeField] private Animator _epilepsieAnimator;

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
                    ShowEpilepsiePanel();
                }
                else if (VideoIsOver())
                {
                    ShowEpilepsiePanel();
                }
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

        private void ShowEpilepsiePanel()
        {
            StartCoroutine(ShowEpilepsiePanelForXSeconds());
        }

        private IEnumerator ShowEpilepsiePanelForXSeconds()
        {
            _epilepsiePanel.SetActive(true);

            TriggerEpilepsiePanelFadeIn();
            yield return new WaitForSeconds(1f);

            var epilepsiePanelShowing = true;
            var canSkipEpilepsiePanel = false;
            var timer = 0f;
            while (epilepsiePanelShowing)
            {
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    canSkipEpilepsiePanel = true;
                }
                if (timer >= 3f)
                {
                    epilepsiePanelShowing = false;
                }
                if (canSkipEpilepsiePanel && Input.anyKeyDown)
                {
                    epilepsiePanelShowing = false;
                }
            }
            TriggerEpilepsiePanelFadeOut();
            yield return new WaitForSeconds(1f);

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
            SceneManager.LoadScene(_nextSceneName);
        }
    }
}
