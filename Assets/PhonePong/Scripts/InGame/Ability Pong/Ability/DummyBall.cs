// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class DummyBall : Ball
{
    protected override void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        SetDontDestroyOnGoal();
    }

    protected override void SetDontDestroyOnGoal() => dontDestroyOnGoal = false;

    public void SetVelocity(Vector2 velocity)
    {
        rb2D.linearVelocity = velocity.normalized * CurrentSpeed;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == racketLayer)
        {
            Destroy(gameObject);
        }
        else
        {
            rb2D.linearVelocity = rb2D.linearVelocity.normalized * CurrentSpeed;
        }
    }
}
