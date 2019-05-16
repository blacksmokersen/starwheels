using UnityEngine;

namespace KartMeshes
{
    public class KartMeshSetter : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Multiplayer.PlayerSettings _playerSettings;

        [Header("Meshes")]
        [SerializeField] private GameObject _kartMesh0;
        [SerializeField] private GameObject _kartMesh1;
        [SerializeField] private GameObject _kartMesh2;

        private GameObject[] _karts;

        private void Awake()
        {
            _karts = new GameObject[3] { _kartMesh0, _kartMesh1, _kartMesh2 };
            int kartChoiceIndex = _playerSettings ? _playerSettings.KartIndex : 0 ;
            var myKartChoice = Instantiate(_karts[kartChoiceIndex]);
            myKartChoice.transform.SetParent(_parent);
            myKartChoice.transform.localPosition = Vector3.zero;
        }
    }
}
