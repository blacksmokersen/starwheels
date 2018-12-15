using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

namespace Common.Audio
{
    public class SnapshotController : MonoBehaviour
    {
        [Header("Mixer Parameters")]
        [SerializeField] private AudioMixer _mixer;

        [Header("Snapshot Parameters")]
        [SerializeField] private string _snapshotName;
        [SerializeField] private float _fadeInSeconds;
        [SerializeField] private float _fadeOutSeconds;

        private AudioMixerSnapshot _snapshot;
        private AudioMixerSnapshot _defaultSnapshot;

        private void Awake()
        {
            Assert.IsNotNull(_mixer, "Mixer reference not set.");
            Assert.AreNotEqual("", _snapshotName, "Snapshot name not set.");

            _snapshot = _mixer.FindSnapshot(_snapshotName);
            _defaultSnapshot = _mixer.FindSnapshot(Constants.AudioMixer.DefaultSnapshot);
            _defaultSnapshot.TransitionTo(0f);
        }

        // PUBLIC

        public void ActivateSnapshot()
        {
            if (_snapshot)
            {
                _snapshot.TransitionTo(_fadeInSeconds);
            }
        }

        public void StopSnapshot()
        {
            if (_defaultSnapshot)
            {
                _defaultSnapshot.TransitionTo(_fadeOutSeconds);
            }
        }
    }
}
