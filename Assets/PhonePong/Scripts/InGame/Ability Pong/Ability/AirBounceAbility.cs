// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class AirBounceAbility : Ability
{
    [SerializeField] float minAirBounceWaitTime = 1f;
    [SerializeField] float maxAirBounceWaitTime = 1.5f;

    public override void Excute(AbilityRacket racket)
    {
        racket.SetAbility((AbilityBall ball) => UseAbility(ball));
    }

    public void UseAbility(AbilityBall ball)
    {
        ball.StartCoroutine(CoroutineAirBounce(ball.Rb2D));
    }

    private IEnumerator CoroutineAirBounce(Rigidbody2D rb2D)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minAirBounceWaitTime, maxAirBounceWaitTime));

        rb2D.linearVelocityY = -rb2D.linearVelocityY;
    }
}
