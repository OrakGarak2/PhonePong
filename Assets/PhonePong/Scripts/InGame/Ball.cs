// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

// PhonePong
using PhonePong.Layer;

public class Ball : MonoBehaviour
{
    [Header("물리")]
    [SerializeField] protected Rigidbody2D rb2D;

    [Header("이동속도")]
    [SerializeField] private float originalSpeed;
    [SerializeField] protected float currentSpeed;
    [SerializeField] protected float acceleration = 1f;
    protected const float accelerationIncreaseRate = 0.05f;
    protected const float maxSpeed = 40f;
    public float CurrentSpeed { get { return currentSpeed * acceleration <= maxSpeed ? currentSpeed * acceleration : maxSpeed; } }

    [Header("초기 값 설정")]
    [SerializeField][Range(0, 10f)] private float startDirectionRangeX;
    [SerializeField][Range(0, 10f)] private float startDirectionRangeY;

    [Header("파괴 여부")]
    [SerializeField] protected bool dontDestroyOnGoal;
    public bool DontDestroyOnGoal => dontDestroyOnGoal;

    protected const int racketLayer = LayerDatas.racketLayer;
    private const float resetWaitTime = 1f;

    protected Coroutine currentResetCoroutine;

    protected virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        SetDontDestroyOnGoal();

        // startDirectionRangeX가 startDirectionRangeY 이상이어야 시작할 때 공이 가운데에서 오래 머물지 않는다.
        if (startDirectionRangeX < startDirectionRangeY) startDirectionRangeX = startDirectionRangeY;

        Reset();
    }

    protected virtual void SetDontDestroyOnGoal() => dontDestroyOnGoal = true;

    public void Reset()
    {
        if (currentResetCoroutine != null) StopCoroutine(currentResetCoroutine);

        currentResetCoroutine = StartCoroutine(CoroutineReset());
    }

    protected virtual IEnumerator CoroutineReset()
    {
        ResetSpeed();
        rb2D.linearVelocity = Vector2.zero;
        rb2D.position = Vector2.zero;

        yield return new WaitForSeconds(resetWaitTime);

        float startDirectionY = UnityEngine.Random.Range(-startDirectionRangeY, startDirectionRangeY);
        float startDirectionX = UnityEngine.Random.Range(startDirectionY >= 0 ? startDirectionY : -startDirectionRangeX,
                                                        startDirectionY >= 0 ? startDirectionRangeX : startDirectionY);

        Vector2 startDirection = new Vector2(startDirectionX, startDirectionY).normalized;

        rb2D.linearVelocity = startDirection * CurrentSpeed;

        currentResetCoroutine = null;
    }

    public void ResetSpeed()
    {
        currentSpeed = originalSpeed;
        acceleration = 1f;
    }
    private float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == racketLayer)
        {
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
}