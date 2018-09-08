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
        public static GameMode CurrentGameMode;

        private GameObject[] _spawns;

        // CORE

        protected void Start()
        {
            _spawns = GameObject.FindGameObjectsWithTag(Constants.Tag.Spawn);

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.OfflineMode = true;
                PhotonNetwork.LocalPlayer.SetTeam(PunTeams.Team.blue);
                PhotonNetwork.CreateRoom("local");
            }

            SpawnKart(PhotonNetwork.LocalPlayer.GetTeam());
        }

        // PUBLIC

        // PRIVATE

        private void SpawnKart(PunTeams.Team team)
        {
            SceneManager.LoadScene(Constants.Scene.GameHUD, LoadSceneMode.Additive);

            int numberOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;

            for (int i = 0; i < numberOfPlayers; ++i)
            {
                var initPos = _spawns[i].transform.position;
                var initRot = _spawns[i].transform.rotation;
                var kart = PhotonNetwork.Instantiate("Kart", initPos, initRot, 0);

                PhotonView photonView = kart.GetComponent<PhotonView>();

                if (photonView.IsMine)
                {
                    var cinemachineDynamicScript = FindObjectOfType<CinemachineDynamicScript>();
                    cinemachineDynamicScript.Initialize();
                    cinemachineDynamicScript.SetKart(kart);
                }
            }
        }
    }
}
