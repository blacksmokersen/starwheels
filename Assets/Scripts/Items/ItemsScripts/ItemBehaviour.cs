﻿using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace Items
{
    /*
     * Base item class for handling the instantiation and destroy
     *
     */
    public class ItemBehaviour : MonoBehaviourPun
    {
        public ItemData ItemData;

        public virtual void Spawn(KartInventory kart, Direction direction)
        { }

        public void DestroyObject(float timeBeforeDestroy = 0f)
        {
            if (PhotonNetwork.IsConnected)
            {
                StartCoroutine(DelayedDestroy(timeBeforeDestroy));
            }
            else
            {
                UnityEngine.MonoBehaviour.Destroy(gameObject, timeBeforeDestroy);
            }
        }

        private IEnumerator DelayedDestroy(float t)
        {
            yield return new WaitForSeconds(t);
            MultiplayerDestroy();
        }

        private void MultiplayerDestroy()
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.RemoveRPCs(photonView);
                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}
