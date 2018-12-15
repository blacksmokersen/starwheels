using UnityEngine;
using FMOD.Studio;
using FMODUnity;

namespace Common.Audio
{
    public class SnapshotController : MonoBehaviour
    {
        [SerializeField] private string _snapshotName;

        private EventInstance _snapshot;
        private ParameterInstance _parameter;

        private void Start()
        {
            _snapshot = RuntimeManager.CreateInstance("snapshot:/" + _snapshotName);
        }

        // PUBLIC

        public void ActivateSnapshot()
        {
            _snapshot.start();
        }

        public void StopSnapshot()
        {
            _snapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
