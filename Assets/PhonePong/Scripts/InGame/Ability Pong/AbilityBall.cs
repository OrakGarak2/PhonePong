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

    [Header("트레일")]
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Gradient originalGridient;

    [Header("패들(임시)")]
    [SerializeField] private AbilityPaddle[] paddles;

    private event Action resetEvent;

    public Rigidbody2D Rb2D => rb2D;

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        originalColor = spriteRenderer.color;

        foreach (var paddle in paddles)
        {
            AddResetEventListener(paddle.Reset);
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

        resetEvent?.Invoke();
        trailRenderer.enabled = false;

        ResetSpeed();
        ResetAcceleration();

        // 분명 (0, 0)으로 초기화했는데도 가끔씩 velocity가 (0, 0)이 안되고 스스로 튕겨나가는 버그가 일어나서 때문에 고정
        float timer = 0;
        while (timer < resetWaitTime)
        {
            rb2D.position = Vector2.zero;
            rb2D.linearVelocity = Vector2.zero;

            timer += Time.deltaTime;
            yield return null;
        }

        float startDirectionY = UnityEngine.Random.Range(-startDirectionRangeY, startDirectionRangeY);
        float startDirectionX = UnityEngine.Random.Range(startDirectionY >= 0 ? startDirectionY : -startDirectionRangeX,
                                                        startDirectionY >= 0 ? startDirectionRangeX : startDirectionY);

        Vector2 startDirection = new Vector2(startDirectionX, startDirectionY).normalized;

        rb2D.linearVelocity = startDirection * CurrentSpeed;

        currentResetCoroutine = null;
        
        trailRenderer.enabled = true;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == paddleLayer)
        {
            ResetColor();
            ResetSpeed();

            col.transform.GetComponent<AbilityPaddle>().ExcuteAbility(this);

            // 공이 맞은 방향을 계산해서 y 방향 벡터를 구한다.
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);


            float x = col.relativeVelocity.x > 0 ? 1f : -1f; //transform.position.x < col.transform.position.x ? -1f : 1f;

            acceleration += accelerationIncreaseRate;

            rb2D.linearVelocity = new Vector2(x, y);
        }

        rb2D.linearVelocity = rb2D.linearVelocity.normalized * CurrentSpeed;
    }

    public void AddResetEventListener(Action action)
    {
        resetEvent += action;
    }

    public void RemoveResetEventListener(Action action)
    {
        resetEvent -= action;
    }

    public void EmptyResetEvent()
    {
        resetEvent = null;
    }

    private void OnDestroy()
    {
        EmptyResetEvent();
    }
}
