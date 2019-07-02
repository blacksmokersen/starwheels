using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Multiplayer;
using Bolt;
using Common.PhysicsUtils;

namespace Items
{
    public class OverchargeBehaviour : EntityBehaviour<IItemState>
    {
        [Header("Settings")]
        [SerializeField] private OverchargeSettings _settings;

        [Header("Settings")]
        [SerializeField] private BoostSettings _overchargeBoostSettings;

        [Header("Unity Events")]
        public UnityEvent OnActivation;
        public UnityEvent OnDeactivation;

        private Ownership _ownership;
        private GameObject _ownerKart;
        private GameObject _ownerKartHitBox;
        private List<GameObject> _kartsInRange = new List<GameObject>();
        private Dictionary<GameObject, float> _kartsInRangeTimer = new Dictionary<GameObject, float>();

        // MONO

        private void Awake()
        {
            _ownership = GetComponent<Ownership>();
        }

        private void Start()
        {
            Setup();
            state.AddCallback("OwnerID", Setup);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (entity.isAttached && entity.isOwner && other.CompareTag(Constants.Tag.KartHealthHitBox))
            {
                TryAddToLists(other);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (entity.isAttached && entity.isOwner && other.CompareTag(Constants.Tag.KartHealthHitBox))
            {
                TryAddToLists(other);
                IncreaseStayTimer(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (entity.isAttached && entity.isOwner && other.CompareTag(Constants.Tag.KartHealthHitBox))
            {
                TryRemoveFromLists(other);
            }
        }

        // PUBLIC

        public void Setup()
        {
            _ownerKart = _ownership.OwnerKartRoot;

            SetParent();
            if (entity.isOwner)
            {
                SetOwnerNewSpeed();
                StartCoroutine(SelfDeactivationCoroutine());
            }
            SetOwnerInvincibility();
            DisableOwnerInventory();

            OnActivation.Invoke();
        }

        public void DisableOwnerInventory()
        {
            _ownerKart.GetComponentInChildren<Inventory>().Enabled = false;
        }

        public void EnableOwnerInventory()
        {
            _ownerKart.GetComponentInChildren<Inventory>().Enabled = true;
        }

        //PRIVATE

        private void SetParent()
        {
            transform.SetParent(_ownerKart.transform);
            transform.localPosition = Vector3.zero;
        }

        private void SetOwnerNewSpeed()
        {
            var kartBoost = _ownerKart.GetComponentInChildren<Boost>();
            kartBoost.CustomBoostFromBoostSettings(_overchargeBoostSettings);
        }

        private void SetOwnerInvincibility()
        {
            var health = _ownerKart.GetComponentInChildren<Health.Health>();
            health.SetInvincibilityForXSeconds(_settings.OverchargeDuration);
            _ownerKartHitBox = health.GetComponentInChildren<KartCollisionTrigger>().gameObject;
        }

        private IEnumerator SelfDeactivationCoroutine()
        {
            yield return new WaitForSeconds(_settings.OverchargeDuration);
            OnDeactivation.Invoke();
        }

        private void IncreaseStayTimer(Collider other)
        {
            if (_kartsInRangeTimer.ContainsKey(other.gameObject))
            {
                _kartsInRangeTimer[other.gameObject] += Time.deltaTime;

                if (_kartsInRangeTimer[other.gameObject] > _settings.SecondsInRangeBeforeHit)
                {
                    var victimHealth = other.GetComponentInParent<Health.Health>();
                    if (!victimHealth.IsInvincible)
                    {
                        SendPlayerHitAndRemove(other);
                    }
                }
            }
        }

        private void TryAddToLists(Collider other)
        {
            if (other.gameObject != _ownerKartHitBox && !_kartsInRange.Contains(other.gameObject))
            {
                IKartState victimKartState;
                BoltEntity victimEntity = other.GetComponentInParent<BoltEntity>();
                if(victimEntity.TryFindState<IKartState>(out victimKartState))
                {
                    if(victimKartState.Team != (int)_ownership.Team)
                    {
                        _kartsInRange.Add(other.gameObject);
                        _kartsInRangeTimer.Add(other.gameObject, 0f);
                    }
                }
            }
        }

        private void TryRemoveFromLists(Collider other)
        {
            if (_kartsInRange.Contains(other.gameObject))
            {
                _kartsInRange.Remove(other.gameObject);
                _kartsInRangeTimer.Remove(other.gameObject);
            }
        }

        private void SendPlayerHitAndRemove(Collider other)
        {
            IKartState victimKartState;
            BoltEntity victimEntity = other.GetComponentInParent<BoltEntity>();
            if(victimEntity.TryFindState<IKartState>(out victimKartState))
            {
                PlayerHit playerHitEvent = PlayerHit.Create();
                playerHitEvent.KillerID = _ownership.OwnerID;
                playerHitEvent.KillerName = _ownership.OwnerNickname;
                playerHitEvent.KillerTeam = (int) _ownership.Team;
                playerHitEvent.Item = _ownership.Label;
                playerHitEvent.VictimEntity = victimEntity;
                playerHitEvent.VictimID = victimKartState.OwnerID;
                playerHitEvent.VictimName = victimEntity.GetComponent<PlayerInfo>().Nickname;
                playerHitEvent.VictimTeam = (int) victimKartState.Team;
                playerHitEvent.Send();
            }
            else
            {
                Debug.LogError("Could not find the victim's attached state.");
            }
            TryRemoveFromLists(other);
        }

        private void OnDestroy()
        {
            EnableOwnerInventory();
        }
    }
}
