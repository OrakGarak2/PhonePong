// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class PaddleSizeUpAbility : Ability, IPaddleAbility
{
    [SerializeField] private float sizeMultiple = 1.5f;
    [SerializeField] private Coroutine currentCoroutine;
    private const float holdingTime = 2f;

    public override void Excute(AbilityPaddle paddle)
    {
        UseAbility(paddle);
    }

    public void UseAbility(AbilityPaddle paddle)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = paddle.StartCoroutine(CoroutineSizeUp(paddle));

        paddle.SetAbility(null, ResetPaddle);
    }

    private IEnumerator CoroutineSizeUp(AbilityPaddle paddle)
    {
        paddle.ChangeSize(new Vector2(paddle.transform.localScale.x, paddle.transform.localScale.y * sizeMultiple));

        yield return new WaitForSeconds(holdingTime);

        paddle.ResetSize();
    }

    public void ResetPaddle(AbilityPaddle paddle)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        
        paddle.ResetSize();
    }
}
