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

    [Header("라켓(임시)")]
    [SerializeField] private AbilityRacket[] rackets;

    public Rigidbody2D Rb2D => rb2D;

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        base.Start();
    }

    public void ChangeColor(Color newColor) => spriteRenderer.color = newColor;
    public void ResetColor() => spriteRenderer.color = originalColor;
    public void MultiplySpeed(float speedMultiple)
    {
        currentSpeed *= speedMultiple;
        rb2D.linearVelocity = rb2D.linearVelocity.normalized * currentSpeed;
    }
    protected override IEnumerator CoroutineReset()
    {
        ResetColor();

        foreach (var racket in rackets)
        {
            racket.Reset();
        }

        return base.CoroutineReset();
    }
}
