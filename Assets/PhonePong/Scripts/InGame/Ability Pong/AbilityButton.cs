// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] private AbilityButtonGroup abilityButtonGroup;

    [SerializeField] private Ability ability;
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    [SerializeField] private AbilityPaddle abilityPaddle;

    private void Start()
    {
        ability = GetComponent<Ability>();

        button = GetComponent<Button>();
        image = GetComponent<Image>();

        button.onClick.AddListener(OnButtonClicked);
    }

    public Action<float> ObserveAbilityButton(AbilityButtonGroup group, AbilityPaddle paddle)
    {
        abilityButtonGroup = group;
        abilityPaddle = paddle;

        return UpdateCooldown;
    }

    private void OnButtonClicked()
    {
        ability.Excute(abilityPaddle);
        
        abilityButtonGroup.UpdateCooldown(ability.cooldown);
    }

    public void UpdateCooldown(float cooldown)
    {
        StartCoroutine(CoroutineUpdateCooldown(cooldown));
    }

    private IEnumerator CoroutineUpdateCooldown(float cooldown)
    {
        button.interactable = false;

        float timer = 0f;
        while (timer < cooldown)
        {
            timer += Time.deltaTime;
            image.fillAmount = timer / cooldown;

            yield return null;
        }

        button.interactable = true;
    }
}
