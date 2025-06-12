// System
using System;
using System.Collections;
using System.Collections.Generic;

// Unity
using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float speed;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveDirection)
    {
        rb2D.linearVelocityY = moveDirection * speed;
    }
}