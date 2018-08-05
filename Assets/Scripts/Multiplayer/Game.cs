﻿using UnityEngine;
using Cinemachine;
using Photon;

public class Game : PunBehaviour
{
    private void Start()
    {
        Vector3 initPos = Vector3.up;

        GameObject kart = SpawnKart(initPos);        

        if (!PhotonNetwork.connected || kart.GetComponent<PhotonView>().isMine)
        {
            CinemachineVirtualCamera camera = FindObjectOfType<CinemachineVirtualCamera>();
            camera.Follow = kart.transform;
            camera.LookAt = kart.transform;
        }
    }

    public GameObject SpawnKart(Vector3 initPos)
    {
        if (PhotonNetwork.connected)
        {
            return PhotonNetwork.Instantiate("Kart", initPos, Quaternion.identity, 0);
        }
        else
        {
            return Instantiate(Resources.Load<GameObject>("Kart"), initPos, Quaternion.identity);
        }
    }
}
