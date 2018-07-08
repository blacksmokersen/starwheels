using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon;

using Controls;
using HUD;
using Kart;

public class Game : PunBehaviour
{
    private Color[] _colors = new Color[]
    {
        Color.red, Color.blue, Color.black, Color.white, Color.green, Color.yellow, Color.gray, Color.cyan
    };

    private void Start()
    {
        Vector3 initPos = Vector3.up;

        GameObject kart;
        if (PhotonNetwork.connected)
        {
            kart = PhotonNetwork.Instantiate("Kart", initPos, Quaternion.identity, 0);
        }
        else 
        {
            kart = Instantiate(Resources.Load<GameObject>("Kart"), initPos, Quaternion.identity);
        }

        if (!PhotonNetwork.connected || kart.GetComponent<PhotonView>().isMine)
        {
            CinemachineVirtualCamera camera = FindObjectOfType<CinemachineVirtualCamera>();
            camera.Follow = kart.transform;
            camera.LookAt = kart.transform;

            FindObjectOfType<PlayerInputs>().SetKart(kart.GetComponentInChildren<KartActions>());
            FindObjectOfType<HUDUpdater>().SetKart(kart.GetComponentInChildren<Rigidbody>());
        }
    }
}
