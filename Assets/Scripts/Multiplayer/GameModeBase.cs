using System.Collections;
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
            int playerId = PhotonNetwork.LocalPlayer.ActorNumber;

            var initPos = _spawns[playerId].transform.position;
            var initRot = _spawns[playerId].transform.rotation;
            var kart = PhotonNetwork.Instantiate("Kart", initPos, initRot, 0);

            var cinemachineDynamicScript = FindObjectOfType<CinemachineDynamicScript>();
            cinemachineDynamicScript.Initialize();
            cinemachineDynamicScript.SetKart(kart);
            StartCoroutine(LoadGameHUD(kart));
        }

        private IEnumerator LoadGameHUD(GameObject kart)
        {
            AsyncOperation loadLevel = SceneManager.LoadSceneAsync(Constants.Scene.GameHUD, LoadSceneMode.Additive);
            while (!loadLevel.isDone)
            {
                yield return null;
            }
            FindObjectOfType<HUD.GameHUD>().ObserveKart(kart);
        }
    }
}
