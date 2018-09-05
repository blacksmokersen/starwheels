using UnityEngine;
using Cinemachine;
using Photon;
using UnityEngine.SceneManagement;

namespace GameModes
{
    public class GameMode : PunBehaviour
    {
        private GameObject[] _spawns;

        protected void Start()
        {
            _spawns = GameObject.FindGameObjectsWithTag(Constants.SpawnPointTag);
            if (!PhotonNetwork.connected)
            {
                PhotonNetwork.offlineMode = true;
                PhotonNetwork.CreateRoom("Solo");
            }
            SpawnKart(PhotonNetwork.player.GetTeam());
        }

        public void SpawnKart(PunTeams.Team team)
        {
            int numberOfPlayers = PhotonNetwork.countOfPlayers;
            var initPos = _spawns[numberOfPlayers].transform.position;
            var kart = PhotonNetwork.Instantiate("Kart", initPos, _spawns[numberOfPlayers].transform.rotation, 0);
            if (kart.GetComponent<PhotonView>().isMine)
            {
                FindObjectOfType<CinemachineDynamicScript>().SetKart(kart);
                SceneManager.LoadScene(Constants.GameHUDSceneName, LoadSceneMode.Additive);
            }
        }
    }
}
