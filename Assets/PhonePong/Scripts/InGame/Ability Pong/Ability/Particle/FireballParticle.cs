// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class FireballParticle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRigidbody2D;

    public void SetBall(AbilityBall ball)
    {
        ballRigidbody2D = ball.Rb2D;

        SetBearing();

        gameObject.SetActive(true);
    }

    public void Disable() => gameObject.SetActive(false);

    private void Update()
    {
        if (ballRigidbody2D == null) return;

        SetBearing();
    }

    private void SetBearing()
    {
        transform.position = ballRigidbody2D.position;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(ballRigidbody2D.linearVelocityX, -ballRigidbody2D.linearVelocityY) * Mathf.Rad2Deg);
    }
}
