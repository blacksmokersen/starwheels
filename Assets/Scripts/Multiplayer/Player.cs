using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

namespace Multiplayer
{
    public class Player : PunBehaviour
    {
        private void Update()
        {
            if (photonView.isMine)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.position += Vector3.forward * 10f * Time.deltaTime;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.position += Vector3.forward * -10f * Time.deltaTime;
                }
            }
        }
    }
}