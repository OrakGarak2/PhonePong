// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.Enum;

public class FireballAbility : Ability, IBallAbility
{
    [SerializeField] Color ballColor;
    [SerializeField] Gradient colorGradient;

    [SerializeField] FireballParticle particle;

    [SerializeField] float speedMultiple = 2f;

    public override void Excute(AbilityPaddle paddle)
    {
        paddle.SetAbility((AbilityBall ball) => UseAbility(ball));
    }

    public void UseAbility(AbilityBall ball)
    {
        ball.ChangeColor(ballColor, colorGradient);
        ball.MultiplySpeed(speedMultiple);

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.fireBall, ball.transform.position);

        Action action = () => { ball.ResetColor(); ball.ResetSpeed(); particle.Disable(); };
        action += () => { ball.RemoveSkillResetEventListener(action); };

        ball.AddSkillResetEventListener(action);

        particle.SetBall(ball);
    }
}
