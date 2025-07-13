// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

// Utils
using Utils.Animation;

namespace PhonePong.VSRetro.Nonbattle
{
    public class SelectButton : MonoBehaviour
    {
        [SerializeField] private Button selectButton;
        [SerializeField] private Image image;
        [SerializeField] private NonbattlePaddle nonbattlePaddle;

        private const float nextClickCooldown = 0.5f;

        private void Start()
        {
            selectButton = GetComponent<Button>();
            selectButton.onClick.AddListener(OnSelect);

            image = GetComponent<Image>();
        }

        private void OnSelect()
        {
            nonbattlePaddle.OnSelect();
            CooldownButtonClick();
        }

        private void CooldownButtonClick()
        {
            AnimationUtility.SlideAnimation(selectButton, image, nextClickCooldown, 0f, 0f, 1f, image.fillOrigin,
            () => selectButton.interactable = false, () => selectButton.interactable = true);
        }
    }
}