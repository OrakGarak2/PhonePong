// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class PaddleSizeUpAbility : Ability
{
    [SerializeField] private float sizeMultiple = 1.5f;
    private const float holdingTime = 2f;

    public override void Excute(AbilityPaddle paddle)
    {
        paddle.StartCoroutine(CoroutineSizeUp(paddle.transform));
    }

    private IEnumerator CoroutineSizeUp(Transform paddleTransform)
    {
        float localScaleY = paddleTransform.localScale.y;

        paddleTransform.localScale = new Vector2(paddleTransform.localScale.x, localScaleY * sizeMultiple);

        yield return new WaitForSeconds(holdingTime);

        paddleTransform.localScale = new Vector2(paddleTransform.localScale.x, localScaleY);
    }
}
