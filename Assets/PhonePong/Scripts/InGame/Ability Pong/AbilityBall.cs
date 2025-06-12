// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class AbilityBall : Ball
{
    [Header("스프라이트")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color originalColor;

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        base.Start();
    }

    public void ChangeColor(Color newColor) => spriteRenderer.color = newColor;
    public void ResetColor() => spriteRenderer.color = originalColor;

    protected override IEnumerator CoroutineReset()
    {
        ResetColor();
        return base.CoroutineReset();
    }
}
