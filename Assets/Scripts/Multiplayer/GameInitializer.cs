using UnityEngine;
using Cinemachine;
using Photon;

public class GameInitializer : PunBehaviour
{
    public GameObject[] Spawns;

    private void Start()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.offlineMode = true;
            PhotonNetwork.CreateRoom("Solo");
        }

        GameObject kart = SpawnKart();
        if (kart.GetComponent<PhotonView>().isMine)
        {
            CinemachineVirtualCamera camera = FindObjectOfType<CinemachineVirtualCamera>();
            camera.Follow = kart.transform;
            camera.LookAt = kart.transform;
        }
    }

    public GameObject SpawnKart()
    {
        int numberOfPlayers = PhotonNetwork.countOfPlayers;
        var initPos = Spawns[numberOfPlayers].transform.position;
        return PhotonNetwork.Instantiate("Kart", initPos, Spawns[numberOfPlayers].transform.rotation, 0);
    }
}
