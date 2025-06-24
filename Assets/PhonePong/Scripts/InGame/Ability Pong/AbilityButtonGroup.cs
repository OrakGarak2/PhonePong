// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class AbilityButtonGroup : MonoBehaviour
{
    private Action<float> cooldownAction;
    [SerializeField] private AbilityRacket abilityRacket;

    private void Start()
    {
        foreach (var button in GetComponentsInChildren<AbilityButton>())
        {
            cooldownAction += button.ObserveAbilityButton(this, abilityRacket);
        }
    }

    public void UpdateCooldown(float cooldown)
    {
        cooldownAction?.Invoke(cooldown);
    }

    private void OnDisable()
    {
        cooldownAction = null;
    }
}
