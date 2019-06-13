using UnityEngine;
using Multiplayer;

namespace SW.Customization
{
    [DisallowMultipleComponent]
    public class KartMeshSetter : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;

        [Header("Meshes")]
        [SerializeField] private GameObject _kartMesh0;
        [SerializeField] private GameObject _kartMesh1;
        [SerializeField] private GameObject _kartMesh2;

        private GameObject[] _karts;

        // CORE

        private void Awake()
        {
            _karts = new GameObject[3] { _kartMesh0, _kartMesh1, _kartMesh2 };
        }

        // PUBLIC

        public void SetKart(int index)
        {
            for (int i = 0; i < _karts.Length; i++)
            {
                if (i == index)
                {
                    _karts[i].SetActive(true);
                }
                else
                {
                    _karts[i].SetActive(false);
                }
            }
        }
    }
}
