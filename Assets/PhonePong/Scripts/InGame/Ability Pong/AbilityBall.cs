// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// 

public class AbilityBall : Ball
{
    [SerializeField] private Gradient originalGridient;

    [Header("스프라이트")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color originalColor;

    [Header("패들(임시)")]
    [SerializeField] private AbilityPaddle[] paddles;

    private event Action softResetEvent;
    private event Action hardResetEvent;

    public Rigidbody2D Rb2D => rb2D;

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        foreach (var paddle in paddles)
        {
            hardResetEvent += paddle.Reset;
        }

        base.Start();
    }

    public void ChangeColor(Color newColor, Gradient gradient)
    {
        spriteRenderer.color = newColor;
        trailRenderer.colorGradient = gradient;
    }

    public void ResetColor()
    {
        spriteRenderer.color = originalColor;
        trailRenderer.colorGradient = originalGridient;
    }

    public void MultiplySpeed(float speedMultiple)
    {
        currentSpeed *= speedMultiple;
        rb2D.linearVelocity = rb2D.linearVelocity.normalized * CurrentSpeed;
    }
    protected override IEnumerator CoroutineReset()
    {
        ResetColor();

        softResetEvent?.Invoke();
        softResetEvent = null;
        
        hardResetEvent?.Invoke();
        
        return base.CoroutineReset();
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == paddleLayer)
        {
            softResetEvent?.Invoke();
            softResetEvent = null;

            col.transform.GetComponent<AbilityPaddle>().ExcuteAbility(this);

            // 공이 맞은 방향을 계산해서 y 방향 벡터를 구한다.
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);


            float x = col.relativeVelocity.x > 0 ? 1f : -1f; //transform.position.x < col.transform.position.x ? -1f : 1f;

            acceleration += accelerationIncreaseRate;

            rb2D.linearVelocity = new Vector2(x, y);
        }

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ballBounce, transform.position);

        rb2D.linearVelocity = rb2D.linearVelocity.normalized * CurrentSpeed;
    }

    public void AddSkillResetEventListener(Action action)
    {
        softResetEvent += action;
    }

    public void AddHardResetEventListener(Action action)
    {
        hardResetEvent += action;
    }

    public void RemoveHardResetEventListener(Action action)
    {
        hardResetEvent -= action;
    }

    private void OnDestroy()
    {
        softResetEvent = null;
        hardResetEvent = null;
    }
}
