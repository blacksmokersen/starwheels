﻿using UnityEngine;
using TMPro;
using Bolt;

namespace Common.HUD
{
    public class NicknamePanel : EntityBehaviour<IKartState>
    {
        [Header("Elements")]
        [SerializeField] private TextMeshPro nameText;
        [SerializeField] private SpriteRenderer frameRenderer;

        private GameObject _camera;
        private GameSettings _gameSettings;

        // CORE

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
        }

        private void Start()
        {
            _camera = GameObject.Find("PlayerCamera");
        }

        private void Update()
        {
            if (_camera != null)
            {
                transform.LookAt(_camera.transform);
            }
        }

        // BOLT

        public override void ControlGained()
        {
            gameObject.SetActive(false);
        }

        public override void ControlLost()
        {
            gameObject.SetActive(true);
        }

        // PUBLIC

        public void SetName(string name)
        {
            nameText.text = name;
        }

        public void SetFrameRendererColor(Color color)
        {
            frameRenderer.color = color;
        }

        public void SetFrameRendererTeam(Team team)
        {
            if (team != Team.None)
            {
                var color = _gameSettings.TeamsListSettings.GetSettings(team).NameplateColor;
                SetFrameRendererColor(color);
            }
            else
            {
                Debug.LogWarning("No team was found.");
            }
        }

        public void ShowPanel()
        {
            gameObject.SetActive(true);
        }

        public void HidePanel()
        {
            gameObject.SetActive(false);
        }
    }
}
