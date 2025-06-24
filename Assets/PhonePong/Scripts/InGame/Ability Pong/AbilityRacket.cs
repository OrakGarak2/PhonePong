// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.Enum;
using PhonePong.Layer;

public class AbilityRacket : Racket
{
    [SerializeField] PlayerEnum playerEnum;
    public PlayerEnum PlayerEnum => playerEnum;
    private Action<AbilityBall> ballAbilityAction;

    private Vector2 originalScale;

    private const int ballLayer = LayerDatas.ballLayer;

    protected override void Awake()
    {
        base.Awake();
        originalScale = transform.localScale;
    }

    public void SetAbility(Action<AbilityBall> action)
    {
        ballAbilityAction = action;
    }

    public void ExcuteAbility(AbilityBall abilityBall)
    {
        ballAbilityAction?.Invoke(abilityBall);
        ballAbilityAction = null;
    }

    public void Reset()
    {
        ballAbilityAction = null;
        transform.localScale = originalScale;
    }
}
