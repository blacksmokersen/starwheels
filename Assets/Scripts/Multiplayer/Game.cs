using UnityEngine;
using Cinemachine;
using Photon;

public class Game : PunBehaviour
{
    public GameObject[] Spawns;
    Vector3 initPos;

    private void Start()
    {
        Debug.Log("start");
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.offlineMode = true;
            PhotonNetwork.CreateRoom("Solo");
        }
       // Vector3 initPos = Vector3.up;
       
        GameObject kart = SpawnKart(initPos);

        if (kart.GetComponent<PhotonView>().isMine)
        {
            CinemachineVirtualCamera camera = FindObjectOfType<CinemachineVirtualCamera>();
            camera.Follow = kart.transform;
            camera.LookAt = kart.transform;
        }
    }

    public GameObject SpawnKart(Vector3 initPos)
    {
        int numberOfPlayers = PhotonNetwork.countOfPlayers;
        initPos = Spawns[numberOfPlayers].transform.position;
        return PhotonNetwork.Instantiate("Kart", initPos, Spawns[numberOfPlayers].transform.rotation, 0);
    }
}
