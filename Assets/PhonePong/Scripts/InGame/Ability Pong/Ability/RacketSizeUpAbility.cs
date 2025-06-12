// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class RacketSizeUpAbility : Ability
{
    [SerializeField] private float sizeMultiple = 1.5f;
    private const float holdingTime = 2f;

    public override void Excute(AbilityRacket racket)
    {
        racket.StartCoroutine(CoroutineSizeUp(racket.transform));
    }

    private IEnumerator CoroutineSizeUp(Transform racketTransform)
    {
        float localScaleY = racketTransform.localScale.y;

        racketTransform.localScale = new Vector2(racketTransform.localScale.x, localScaleY * sizeMultiple);

        yield return new WaitForSeconds(holdingTime);

        racketTransform.localScale = new Vector2(racketTransform.localScale.x, localScaleY);
    }
}
