// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("기존 색상")]
    [SerializeField] private Color originalColor;

    [Header("이동속도")]
    [SerializeField] private float originalSpeed;
    [SerializeField] private float currentSpeed;

    [Header("초기 값 설정")]
    [SerializeField][Range(0, 10f)] private float startDirectionRangeX;
    [SerializeField][Range(0, 10f)] private float startDirectionRangeY;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Reset();
    }

    public virtual void Reset()
    {
        ResetSpeed();
        ResetColor();
        transform.position = Vector2.zero;

        Vector2 startDirection = new Vector2(
            UnityEngine.Random.Range(-startDirectionRangeX, startDirectionRangeX),
            UnityEngine.Random.Range(-startDirectionRangeY, startDirectionRangeY)).normalized;

        rb2D.linearVelocity = startDirection * currentSpeed;
    }

    public void ResetSpeed() => currentSpeed = originalSpeed;
    public void ResetColor() => spriteRenderer.color = originalColor;
    public void ChangeColor(Color newColor) => spriteRenderer.color = newColor;

    float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Note: 'col' holds the collision information. If the
        // Ball collided with a racket, then:
        //   col.gameObject is the racket
        //   col.transform.position is the racket's position
        //   col.collider is the racket's collider

        // did we hit a racket? then we need to calculate the hit factor
        if (col.gameObject.layer == 6) // TODO: 후에 레이어 수정할 것
        {
            // Calculate y direction via hit Factor
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate x direction via opposite collision
            float x = col.relativeVelocity.x > 0 ? 1 : -1;

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(x, y).normalized;

            // Set Velocity with dir * speed
            rb2D.linearVelocity = dir * currentSpeed;
        }
    }
}