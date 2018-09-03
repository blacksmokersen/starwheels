using UnityEngine;
using Cinemachine;
using Photon;
using UnityEngine.SceneManagement;

namespace GameModes
{
    public class GameMode : PunBehaviour
    {
        public GameObject[] Spawns;
        public string gameHudSceneName;

        private void Start()
        {
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
            var initPos = Spawns[numberOfPlayers].transform.position;
            var kart = PhotonNetwork.Instantiate("Kart", initPos, Spawns[numberOfPlayers].transform.rotation, 0);
            if (kart.GetComponent<PhotonView>().isMine)
            {
                FindObjectOfType<CinemachineDynamicScript>().SetKart(kart);
                SceneManager.LoadScene(gameHudSceneName, LoadSceneMode.Additive);
            }
        }
    }
}
