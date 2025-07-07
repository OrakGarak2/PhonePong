// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class DummyBall : Ball
{
    private AbilityBall abilityBall;

    protected override void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        SetDontDestroyOnGoal();
    }

    protected override void SetDontDestroyOnGoal() => dontDestroyOnGoal = false;

    public void SetDummyBall(AbilityBall abilityBall, Vector2 velocity)
    {
        this.abilityBall = abilityBall;

        this.abilityBall.AddResetEventListener(Reset);

        rb2D.linearVelocity = velocity.normalized * CurrentSpeed;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == paddleLayer)
        {
            if (currentResetCoroutine != null)
            {
                StopCoroutine(currentResetCoroutine);
                currentResetCoroutine = null;
            }
            Destroy(gameObject);
        }
        else
        {
            rb2D.linearVelocity = rb2D.linearVelocity.normalized * CurrentSpeed;
        }
    }

    public override void Reset()
    {
        currentResetCoroutine = StartCoroutine(CoroutineReset());
    }

    protected override IEnumerator CoroutineReset()
    {
        yield return new WaitForSeconds(1f);

        if(gameObject.activeSelf)
            Destroy(gameObject);
    }

    private void OnDisable()
    {
        abilityBall.RemoveResetEventListener(Reset);
    }
}
