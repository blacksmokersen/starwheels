﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class Compteur : MonoBehaviour
    {
        public Image speedBar;
        public Text textSpeed;

        private string speedString;
        private float speed;
        private float velocityMagnitude;

        public void CompteurBehaviour(float kartVelocity)
        {
            speed = Mathf.Round(velocityMagnitude);
            velocityMagnitude = kartVelocity;
            speedBar.fillAmount = speed / 70;
            SpeedOnScreen(speed);
        }

        private void SpeedOnScreen(float speed)
        {
            speedString = ("" + speed * 2);
            textSpeed.text = speedString.ToString();
        }
    }
}