using UnityEngine;
using Multiplayer;

namespace KBA.Debug
{
    public class DummyInstantiation : MonoBehaviour, IControllable
    {
        [Header("Karts")]
        [SerializeField] private GameObject blueKart;
        [SerializeField] private GameObject redKart;

        // CORE

        private void Update()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Instantiate(blueKart);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                Instantiate(redKart);
        }

        // PRIVATE

        private void InstantiateKart(GameObject kart)
        {
            var spawnPosition = transform.position;
            BoltNetwork.Instantiate(kart, spawnPosition, Quaternion.identity);
        }
    }
}
