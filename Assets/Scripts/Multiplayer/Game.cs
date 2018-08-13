using UnityEngine;
using Cinemachine;
using Photon;

public class Game : PunBehaviour
{
    private void Start()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.offlineMode = true;
            PhotonNetwork.CreateRoom("Solo");
        }
        Vector3 initPos = Vector3.up;

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
        return PhotonNetwork.Instantiate("Kart", initPos, Quaternion.identity, 0);
    }
}
