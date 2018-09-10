using UnityEngine.SceneManagement;
using UnityEngine;
using Abilities;
using Photon.Pun;
using Items;

namespace Controls
{
    public class DebugInputs : BaseKartComponent
    {
        private bool _enabled = true;
        private int _currentItemIndex = 0;
        private GameObject _kart;

        // CORE

        private new void Awake()
        {
            base.Awake();

            if (PhotonNetwork.IsConnected)
            {
                foreach (GameObject kart in GameObject.FindGameObjectsWithTag(Constants.Tag.Kart))
                {
                    if (kart.GetComponent<PhotonView>().IsMine)
                    {
                        _kart = kart;
                        break;
                    }
                }
            }
            else
            {
                _kart = GameObject.FindGameObjectWithTag(Constants.Tag.Kart);
            }
        }

        private void Update()
        {
            if (!_enabled || !photonView.IsMine) return;

            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            // Items
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ResetLives();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchToNextItem();
            }

            // Health
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                LoseOneLife();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                DestroyKart();
            }

            // Maps
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                _kart.transform.position = new Vector3(0,0.1f,0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                _kart.transform.position = new Vector3(-221, 3, 0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                _kart.transform.position = new Vector3(400, 3, 0);
            }

            // Abilities
            if (Input.GetButtonDown(Constants.Input.UseAbility))
            {
                GetComponent<GamepadVibrations>().SmallVibration();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                ReplaceAbility().AddComponent<AbilityTPBack>();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                ReplaceAbility().AddComponent<AbilityJump>();
            }
        }

        // PUBLIC

        // PRIVATE

        private void SwitchToNextItem()
        {
            var items = ItemsLottery.Items;

            kartHub.kartInventory.SetItem(items[(_currentItemIndex++) % items.Length], 1000);
        }

        private void LoseOneLife()
        {
            kartHub.kartHealthSystem.HealthLoss();
        }

        private void ResetLives()
        {
            kartHub.kartHealthSystem.ResetLives();
        }

        private GameObject ReplaceAbility()
        {
            Destroy(_kart.GetComponentInChildren<Ability>());

            return _kart.transform.GetChild(0).gameObject;
        }

        private void DestroyKart()
        {
            GetComponent<PhotonView>().RPC("RPCDestroy", RpcTarget.AllBuffered);
        }

        [PunRPC]
        private void RPCDestroy()
        {
            kartHub.kartEvents.OnKartDestroyed();
        }
    }
}
