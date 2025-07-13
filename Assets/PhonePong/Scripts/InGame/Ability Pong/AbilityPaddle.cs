// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.Enum;
using PhonePong.Layer;

public class AbilityPaddle : Paddle
{
    [SerializeField] PlayerEnum playerEnum;
    public PlayerEnum PlayerEnum => playerEnum;
    private Action<AbilityBall> ballAbilityAction;
    private Action<AbilityPaddle> resetAction;

    private Vector2 originalScale;

    private const int ballLayer = LayerDatas.ballLayer;

    protected override void Awake()
    {
        base.Awake();
        originalScale = transform.localScale;
    }

    public void SetAbility(Action<AbilityBall> abilityAction)
    {
        ballAbilityAction = abilityAction;
    }

    public void SetAbility(Action<AbilityBall> abilityAction, Action<AbilityPaddle> paddleResetAction)
    {
        ballAbilityAction = abilityAction;
        resetAction += paddleResetAction;
    }

    public void ExcuteAbility(AbilityBall abilityBall)
    {
        ballAbilityAction?.Invoke(abilityBall);
        ballAbilityAction = null;
    }

    public void Reset()
    {
        ballAbilityAction = null;

        resetAction?.Invoke(this);
        resetAction = null;
    }

    public void ChangeSize(Vector2 newSize) => transform.localScale = newSize;
    public void ResetSize() => transform.localScale = originalScale;
}
