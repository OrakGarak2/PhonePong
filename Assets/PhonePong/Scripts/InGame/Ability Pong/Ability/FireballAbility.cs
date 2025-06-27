// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.Enum;

public class FireballAbility : Ability
{
    [SerializeField] Color ballColor;
    [SerializeField] float speedMultiple = 2f;

    public override void Excute(AbilityPaddle paddle)
    {
        paddle.SetAbility((AbilityBall ball) => UseAbility(ball));
    }

    public void UseAbility(AbilityBall ball)
    {
        ball.ChangeColor(ballColor);
        ball.MultiplySpeed(speedMultiple);
    }
}
