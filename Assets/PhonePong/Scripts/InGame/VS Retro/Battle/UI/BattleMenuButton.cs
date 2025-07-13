// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.VSRetro.Tooltip;

namespace PhonePong.VSRetro.Battle
{
    public class BattleMenuButton : VSRetroBattleButton
    {
        [SerializeField] private GameObject battleMenuButtonGroup;
        [SerializeField] private GameObject content;

        protected override void Start()
        {
            base.Start();
            battleMenuButtonGroup = transform.parent.gameObject;
        }

        protected override void SelectThis()
        {
            Debug.Log($"{gameObject.name} 선택");
            content.SetActive(true);
            battleMenuButtonGroup.SetActive(false);
        }
    }
}