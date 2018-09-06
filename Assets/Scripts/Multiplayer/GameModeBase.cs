using UnityEngine;
using Photon.Pun.UtilityScripts;
using Photon.Pun;
using UnityEngine.SceneManagement;
using CameraUtils;

namespace GameModes
{
    public enum GameMode { None, ClassicBattle, BankRobbery, GoldenTotem }

    public class GameModeBase : MonoBehaviourPun
    {
        public static GameMode ActualGameMode;

        private GameObject[] _spawns;

        protected void Start()
        {
            _spawns = GameObject.FindGameObjectsWithTag(Constants.SpawnPointTag);
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.OfflineMode = true;
                PhotonNetwork.LocalPlayer.SetTeam(PunTeams.Team.blue);
            }
            SpawnKart(PhotonNetwork.LocalPlayer.GetTeam());
        }

        public void SpawnKart(PunTeams.Team team)
        {
            SceneManager.LoadScene(Constants.GameHUDSceneName, LoadSceneMode.Additive);
            int numberOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            var initPos = _spawns[numberOfPlayers].transform.position;
            var kart = PhotonNetwork.Instantiate("Kart", initPos, _spawns[numberOfPlayers].transform.rotation, 0);
            if (kart.GetComponent<PhotonView>().IsMine)
            {
                var cinemachineDynamicScript = FindObjectOfType<CinemachineDynamicScript>();
                cinemachineDynamicScript.SetKart(kart);
                cinemachineDynamicScript.Initialize();
            }
        }
    }
}
