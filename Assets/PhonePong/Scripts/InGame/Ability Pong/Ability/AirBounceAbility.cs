// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class AirBounceAbility : Ability, IBallAbility
{
    [SerializeField] float minAirBounceDistance = 8f;
    [SerializeField] float maxAirBounceDistance = 10f;

    public override void Excute(AbilityPaddle paddle)
    {
        paddle.SetAbility((AbilityBall ball) => UseAbility(ball));
    }

    public void UseAbility(AbilityBall ball)
    {
        ball.StartCoroutine(CoroutineAirBounce(ball.Rb2D));
    }

    private IEnumerator CoroutineAirBounce(Rigidbody2D rb2D)
    {
        Vector2 beforePos = rb2D.position;
        float bounceDistance = UnityEngine.Random.Range(minAirBounceDistance, maxAirBounceDistance);
        float movingDistance = 0f;

        while (movingDistance < bounceDistance)
        {
            movingDistance += Vector2.Distance(rb2D.position, beforePos);
            Debug.Log($"movingDistance: {movingDistance}, bounceDistance: {bounceDistance}");
            beforePos = rb2D.position;
            yield return null;
        }

        float minusSignVelocityY = Mathf.Sign(-rb2D.linearVelocityY);
        float maxBouncedVelocityY = Mathf.Abs(rb2D.linearVelocityX) + Mathf.Abs(rb2D.linearVelocityY);

        // velocity의 y가 x 이상이도록 설정(적어도 45도로 날아감.)
        Vector2 direction = new Vector2(rb2D.linearVelocityX, UnityEngine.Random.Range(Mathf.Abs(rb2D.linearVelocityX), maxBouncedVelocityY) * minusSignVelocityY).normalized;

        rb2D.linearVelocity = rb2D.linearVelocity.magnitude * direction;
    }
}
