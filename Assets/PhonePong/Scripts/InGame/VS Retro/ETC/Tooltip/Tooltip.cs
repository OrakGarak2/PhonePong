// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// TMP
using TMPro;
using UnityEngine.InputSystem.LowLevel;

namespace PhonePong.VSRetro.Tooltip
{
    public class Tooltip : MonoBehaviour
    {
        private static Tooltip instance;
        public static Tooltip Instance => instance;

        [SerializeField] TMP_Text nameText;
        [SerializeField] TMP_Text descriptionText;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy() => instance = null;

        public void ShowTooltip(Vector2 pos, string name, string desc)
        {
            if (gameObject.activeSelf) return;

            transform.position = pos;

            nameText.text = name;
            descriptionText.text = desc;

            gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }
    }
}